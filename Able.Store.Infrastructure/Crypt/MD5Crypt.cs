using System.Text;
namespace Able.Store.Infrastructure.Crypt
{
    public class MD5Crypt : IHashCrypt
    {
        public byte[] Crypt(string sourceChar, 
            Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(sourceChar);
            System.Security.Cryptography.MD5CryptoServiceProvider check;
            check = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] somme = check.ComputeHash(buffer);
            return somme;
        }
    }
}
