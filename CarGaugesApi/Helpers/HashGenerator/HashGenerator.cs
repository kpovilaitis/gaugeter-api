using System.Security.Cryptography;
using System.Text;

namespace CarGaugesApi.Helpers.HashGenerator
{
    public class HashGenerator : IHashGenerator
    {
        public string ComputeSha256Hash(string rawData)
        {
            using (var hash = SHA256.Create())
            {
                return ComputeHash(rawData, hash);
            }
        }

        public string ComputeSha1Hash(string rawData)
        {
            using (var hash = SHA1.Create())
            {
                return ComputeHash(rawData, hash);
            }
        }

        private string ComputeHash(string rawData, HashAlgorithm algorithm)
        {
            byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var builder = new StringBuilder();
            foreach (var item in bytes)
                builder.Append(item.ToString("x2"));

            return builder.ToString();
        }
    }
}
