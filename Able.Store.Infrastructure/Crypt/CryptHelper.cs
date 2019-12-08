using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Crypt
{
    public  class  CryptHelper
    {
        static Dictionary<HashCryptType, IHashCrypt> _dic = new Dictionary<HashCryptType, IHashCrypt>();

        static CryptHelper()
        {
            _dic = new Dictionary<HashCryptType, IHashCrypt>();

            _dic.Add(HashCryptType.MD5,new MD5Crypt());

            _dic.Add(HashCryptType.SHA1, new SHA1Crypt());
        }
        public static byte[] HashCrypt(HashCryptType hashCrypt,
            string sourceCode, Encoding encoding)
        {
           var ihashCrypt= _dic[hashCrypt];

           var data=  ihashCrypt.Crypt(sourceCode,encoding);

            return data;
        }
    }
}
