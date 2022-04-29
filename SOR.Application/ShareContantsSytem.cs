using System.Text;
using System.Security.Cryptography;

namespace SOR.Application
{
    public class ShareContantsSytem
    {
        public static string MD5(string pPass)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashData = md5.ComputeHash(Encoding.UTF8.GetBytes(pPass));
            var sb = new StringBuilder();
            foreach (var hashByte in hashData)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            return sb.ToString();
        }

        public static string SHA1(string pPass)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hashData = sha1.ComputeHash(Encoding.UTF8.GetBytes(pPass));
            var sb = new StringBuilder();
            foreach (var hashByte in hashData)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }
            return sb.ToString();
        }

        public static string SHA256(string src)
        {
            using (SHA512 hash = SHA512.Create())
            {
                byte[] hashData = hash.ComputeHash(Encoding.UTF8.GetBytes(src));
                var sb = new StringBuilder();
                foreach (var hashByte in hashData)
                {
                    sb.AppendFormat("{0:x2}", hashByte);
                }
                return sb.ToString();
            }
        }
    }
}
