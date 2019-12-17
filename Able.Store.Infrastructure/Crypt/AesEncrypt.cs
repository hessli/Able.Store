using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Able.Store.Infrastructure.Crypt
{
    public class AesEncrypt : IEncrypted
    {
        CipherMode _cipherModel;
        PaddingMode _paddingMode;
        System.Text.Encoding _encoding;
        //AesCryptoServiceProvider 只支持128位，128位 固定的 密匙长度是24,16,32向量是:16
        //RijndaelManaged 类 支持128, 192, or 256  但是在.net core中也只支持128
        private static readonly int SECRETKEYLENTH16 = 16;
        private static readonly int SECRETKEYLENTH24 = 24;
        private static readonly int SECRETKEYLENTH32 = 32;

        private static readonly System.Text.Encoding DEFAULTENCODING = System.Text.Encoding.GetEncoding("gb2312");

        private static readonly int IVLENGTH = 16;
        string _iv;
        private string _secretKey;
        private Base64 _base64;
        public AesEncrypt():this("","")
        {
              
        }
        public AesEncrypt(string secretKey,string iv) :
            this(secretKey, iv, DEFAULTENCODING, CipherMode.ECB, PaddingMode.PKCS7)
        {

        }
        /// <param name="source">源字符</param>
        /// <param name="secretKey">密匙</param>
        /// <param name="iv">向量</param>
        /// <param name="length">加密长度</param>
        /// <param name="model"></param>
        /// <param name="paddingMode"></param>
        /// <param name="encoding"></param>
        public AesEncrypt(string secretKey, string iv,System.Text.Encoding encoding,
            CipherMode model = CipherMode.ECB,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            _secretKey = secretKey;

            _cipherModel = model;

            _paddingMode = paddingMode;

            _iv = iv;
            if (encoding == null)
            {
                _encoding = DEFAULTENCODING;
            }else 
            _encoding = encoding;

            _base64 = new Base64(_encoding);
        }
        private AesCryptoServiceProvider GetAesCryptoServiceProvider()
        {
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            if (!string.IsNullOrWhiteSpace(this._iv))
            {
                var ivArray= this.GetBytes(this._iv, IVLENGTH);

                if (ivArray.Length != IVLENGTH)
                {
                    throw new ArgumentException(string.Format("向量字节码长度必须等于{0}位",IVLENGTH));
                }
                aesProvider.IV = ivArray;
            }
            if (!string.IsNullOrWhiteSpace(this._secretKey))
            {
                var keArray = this.GetBytes(this._secretKey, SECRETKEYLENTH24);
                if (keArray.Length != SECRETKEYLENTH16 && keArray.Length != 
                    SECRETKEYLENTH24 && keArray.Length != SECRETKEYLENTH32)
                {
                    throw new ArgumentException(string.Format("密匙字节码长度必须等于{0}位或{1}或{2}", 
                        SECRETKEYLENTH16,
                        SECRETKEYLENTH24, 
                        SECRETKEYLENTH32));
                }

                aesProvider.Key = keArray;
            }
            aesProvider.Padding = this._paddingMode;

            aesProvider.Mode = this._cipherModel;

            return aesProvider;
        }
    
        byte[] GetBytes(string value, int length)
        {
            var array= Convert.FromBase64String(value);

            return array;
        }

        byte[] GetCrptBytes(string source, ICryptoTransform transform)
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt,_encoding))
                    {
                        swEncrypt.Write(source);
                    }
                   var data=  msEncrypt.ToArray();

                    return data;
                }
            }
        }
        /// <summary>
        /// 通过解密后的字节数组
        /// 获取字符串
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        string GetDecryptStr(byte[] cipherText, ICryptoTransform transform)
        {
            string plaintext = "";
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt,_encoding))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public byte[] CryptByte(string source)
        {
            using (AesCryptoServiceProvider aesProvider = GetAesCryptoServiceProvider())
            {
                UnicodeEncoding enc = new UnicodeEncoding(true, false, false);
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = GetCrptBytes(source, cryptoTransform);

                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return results;
                }
            }
        }
        /// <summary>
        /// 通过base64编码返回
        /// 加密后的字节码的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string CryptStr(string source)
        {
            byte[] data = CryptByte(source);
            return  Convert.ToBase64String(data);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <returns></returns>
        public byte[] DecryptBytes(string source)
        {
            using (var aesProvider = GetAesCryptoServiceProvider())
            {
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source); 
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return results;
                }
            }
        }
        public string DecryptStr(string source)
        {
            using (var aesProvider = GetAesCryptoServiceProvider())
            {
                using (ICryptoTransform decryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);

                    byte[] results = decryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                 
                    var result=this.GetDecryptStr(results, decryptoTransform);

                    aesProvider.Clear();

                    return result;
                }
            }
        }

     
    }
}
