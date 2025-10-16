using Application.Commands.UserModule.Login;
using Application.Common.Interfaces;
using Domain.AggregatesModel.SessionAggregate;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Specifications;
using Domain.AggregatesModel.VerificationAggregate;
using Domain.AggregatesModel.VerificationAggregate.Specifications;
using Domain.SeedWork;
using Moq;
using SharedKernel.Common;
using Xunit;

namespace Application.UnitTests
{
    [Collection("TestCollection")]
    public class LoginHandlerTests
    {
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IRepository<UserSession>> _mockUserSessionRepository;
        private readonly Mock<IRepository<UserVerificationToken>> _mockUserVerificationTokenRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly LoginHandler _handler;

        public LoginHandlerTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockUserSessionRepository = new Mock<IRepository<UserSession>>();
            _mockUserVerificationTokenRepository = new Mock<IRepository<UserVerificationToken>>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _handler = new LoginHandler(
                _mockUserRepository.Object,
                _mockUserSessionRepository.Object,
                _mockUserVerificationTokenRepository.Object,
                _mockPasswordHasher.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsNotRegistered()
        {
            // Arrange
            var command = new LoginCommand("nonexistent@example.com", "password", "device", "ip");
            _mockUserRepository.Setup(
                r => r.FirstOrDefaultAsync(
                    It.IsAny<UserByEmailSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((User?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email is not registered. Please sign up first.", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsNotVerifiedAndNoExistingToken()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "hashedpassword");
            // Use reflection to set EmailVerifiedAt to null for testing purposes
            typeof(User).GetProperty(nameof(User.EmailVerifiedAt))?.SetValue(user, null);

            var command = new LoginCommand("test@example.com", "password", "device", "ip");

            _mockUserRepository.Setup(
                r => r.FirstOrDefaultAsync(
                    It.IsAny<UserByEmailSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(user);

            _mockUserVerificationTokenRepository.Setup(
                r => r.AnyAsync(
                    It.IsAny<UserVerificationTokenByUserId>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(false); // No existing token

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email is not verified. Please verify your email before logging in.", result.Error);

            _mockUserVerificationTokenRepository.Verify(
                r => r.AddAsync(
                    It.IsAny<UserVerificationToken>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once // hàm AddAsync được gọi một lần để tạo token mới
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsNotVerifiedAndExistingToken()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "hashedpassword");
            typeof(User).GetProperty(nameof(User.EmailVerifiedAt))?.SetValue(user, null);

            var command = new LoginCommand("test@example.com", "password", "device", "ip");
            _mockUserRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);
            _mockUserVerificationTokenRepository.Setup(r => r.AnyAsync(It.IsAny<UserVerificationTokenByUserId>(), It.IsAny<CancellationToken>()))
                                                .ReturnsAsync(true); // Existing token

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email is not verified. Please verify your email before logging in.", result.Error);
            _mockUserVerificationTokenRepository.Verify(r => r.AddAsync(It.IsAny<UserVerificationToken>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserIsDisabled()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "hashedpassword");
            user.MarkEmailAsVerified();
            user.Disabled(); // Disable the user

            var command = new LoginCommand("test@example.com", "password", "device", "ip");
            _mockUserRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User account is disabled. Please contact admin.", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLocalAccountIsNotSetUp()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "hashedpassword");
            user.MarkEmailAsVerified();
            // Simulate no local account by clearing auth providers and adding a non-local one
            typeof(User).GetField("_userAuthProviders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(user, new List<UserAuthProvider>());
            user.AddAuthProvider(AuthProvider.Google);

            var command = new LoginCommand("test@example.com", "password", "device", "ip");
            _mockUserRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Local account is not set up for this user. Please use external login.", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "correcthashedpassword");
            user.MarkEmailAsVerified();

            var command = new LoginCommand("test@example.com", "incorrectpassword", "device", "ip");
            _mockUserRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);
            _mockPasswordHasher.Setup(h => h.VerifyPassword(command.Password, It.IsAny<string>()))
                               .Returns(false); // Incorrect password

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Password is incorrect. Please try again.", result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenLoginIsValid()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "correcthashedpassword");
            user.MarkEmailAsVerified();

            var command = new LoginCommand("test@example.com", "password", "device", "ip");
            _mockUserRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UserByEmailSpecification>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);
            _mockPasswordHasher.Setup(h => h.VerifyPassword(command.Password, It.IsAny<string>()))
                               .Returns(true); // Correct password

            _mockUserSessionRepository.Setup(r => r.AddAsync(It.IsAny<UserSession>(), It.IsAny<CancellationToken>()))
                                       .ReturnsAsync((UserSession session, CancellationToken token) => session);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            _mockUserSessionRepository.Verify(r => r.AddAsync(It.IsAny<UserSession>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
