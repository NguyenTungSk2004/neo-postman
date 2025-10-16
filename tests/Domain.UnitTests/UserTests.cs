using Domain.AggregatesModel.UserAggregate;
using Domain.Common.Exceptions;

namespace Domain.UnitTests
{
    [Collection("TestCollection")]
    public class UserTests
    {
        [Fact]
        public void CreateLocalAccount_ShouldCreateUserWithLocalAuthProvider()
        {
            // Arrange
            string name = "Test User";
            string email = "test@example.com";
            string passwordHash = "hashedpassword";

            // Act
            var user = User.CreateLocalAccount(name, email, passwordHash);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email.Address);
            Assert.False(user.IsDisabled);
            Assert.Null(user.EmailVerifiedAt);
            Assert.Single(user.UserAuthProviders);
            Assert.Equal(AuthProvider.Local, user.UserAuthProviders.First().Provider);
            Assert.Equal(passwordHash, user.UserAuthProviders.First().PasswordHash);
        }

        [Fact]
        public void Update_ShouldUpdateUserNameAndAvatarUrl()
        {
            // Arrange
            var user = User.CreateLocalAccount("Old Name", "old@example.com", "password");
            string newName = "New Name";
            string newUrlAvatar = "http://newavatar.com";

            // Act
            user.Update(newName, newUrlAvatar);

            // Assert
            Assert.Equal(newName, user.Name);
            Assert.Equal(newUrlAvatar, user.UrlAvatar);
        }

        [Fact]
        public void MarkEmailAsVerified_ShouldSetEmailVerifiedAt()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "password");

            // Act
            user.MarkEmailAsVerified();

            // Assert
            Assert.NotNull(user.EmailVerifiedAt);
        }

        [Fact]
        public void MarkEmailAsVerified_ShouldThrowExceptionIfAlreadyVerified()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "password");
            user.MarkEmailAsVerified(); // Verify once

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => user.MarkEmailAsVerified());
            Assert.Equal($"Email {user.Email.Address} has been verified.", exception.Message);
        }

        [Fact]
        public void AddAuthProvider_ShouldAddNewUrlAuthProvider()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "password");

            // Act
            user.AddAuthProvider(AuthProvider.Google);

            // Assert
            Assert.Equal(2, user.UserAuthProviders.Count);
            Assert.Contains(user.UserAuthProviders, p => p.Provider == AuthProvider.Google);
        }

        [Fact]
        public void AddAuthProvider_ShouldThrowExceptionIfProviderAlreadyExists()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "password");

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => user.AddAuthProvider(AuthProvider.Local));
            Assert.Equal($"Auth provider {AuthProvider.Local} already exists for user {user.Email.Address}.", exception.Message);
        }

        [Fact]
        public void ChangePassword_ShouldUpdateLocalAccountPassword()
        {
            // Arrange
            var user = User.CreateLocalAccount("Test User", "test@example.com", "oldpassword");
            string newPasswordHash = "newhashedpassword";

            // Act
            user.ChangePassword(newPasswordHash);

            // Assert
            Assert.Equal(newPasswordHash, user.UserAuthProviders.First(p => p.Provider == AuthProvider.Local).PasswordHash);
        }
    }
}
