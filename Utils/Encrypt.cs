using System.Security.Cryptography;
using System.Text;

namespace Notes.Utils
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            MD5 MD5Hash = MD5.Create();

            byte[] passwordBytes = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            string passwordEncrypted = string.Empty;

            foreach (var bytes in passwordBytes)
            {
                passwordEncrypted += bytes;
            }

            return passwordEncrypted.ToString();
        }
    }
}