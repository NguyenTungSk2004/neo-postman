using System.Text.RegularExpressions;

namespace Domain.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Kiểm tra định dạng email hợp lệ.
        /// </summary>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            return false;

            // Regex pattern để kiểm tra định dạng email hợp lệ
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
