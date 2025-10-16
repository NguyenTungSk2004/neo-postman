using Infrastructure.Services;
using Xunit;

namespace Infrastructure.UnitTests
{
    [Collection("TestCollection")]
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordHasherTests()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword()
        {
            // Arrange
            string password = "MySecretPassword123";

            // Act
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Assert
            Assert.False(string.IsNullOrEmpty(hashedPassword));
            Assert.NotEqual(password, hashedPassword); // Hashed password should not be the same as plain password
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
        {
            // Arrange
            string password = "MySecretPassword123";
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Act
            bool isVerified = _passwordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.True(isVerified);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
        {
            // Arrange
            string correctPassword = "MySecretPassword123";
            string incorrectPassword = "WrongPassword";
            string hashedPassword = _passwordHasher.HashPassword(correctPassword);

            // Act
            bool isVerified = _passwordHasher.VerifyPassword(incorrectPassword, hashedPassword);

            // Assert
            Assert.False(isVerified);
        }

        [Fact]
        public void VerifyPassword_ShouldThrowSaltParseException_ForInvalidHash()
        {
            // Arrange
            string password = "MySecretPassword123";
            string invalidHash = "invalidhash"; // Not a valid BCrypt hash

            // Act & Assert
            Assert.Throws<BCrypt.Net.SaltParseException>(() => _passwordHasher.VerifyPassword(password, invalidHash));
        }
    }
}
