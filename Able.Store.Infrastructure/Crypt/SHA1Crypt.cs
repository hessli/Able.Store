using System.Security.Cryptography;
using System.Text;

namespace Able.Store.Infrastructure.Crypt
{
    public class SHA1Crypt:IHashCrypt
    {
        public byte[] Crypt(string sourceChar, Encoding encoding)
        {
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] bytes_in = encoding.GetBytes(sourceChar);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                return bytes_out;
            }
        }
    }
}
