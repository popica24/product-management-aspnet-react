using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Helpers
{
    public static class PasswordHelper
    {
        public static string ComputePassssword(string rawPassword)
        {
            using SHA256 hash = SHA256.Create();

            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
