using Able.Store.Infrastructure;
using Able.Store.Infrastructure.Crypt;
using Able.Store.Infrastructure.Utils;
using System;
namespace Able.Store.IService
{
    public class TokenModel
    {
        private static readonly string DEFAULTKEY = GB2312Base64Helper.Base64EnCode("\u0006aeffssXsfkesge\u001f");
        private static readonly string DEFAULTIV = GB2312Base64Helper.Base64EnCode("\u0017搻\u0017KYZKFRMNVBGF");
     
        public TokenModel()
        {

        }
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
        public int userId { get; private set; }
        public string nick { get; private set; }
        /// <summary>
        /// 剩余有效期
        /// </summary>
        public TimeSpan expiries { get; private set; }

        public string ToToken()
        {
            IEncrypted encrypted = new AesEncrypt(DEFAULTKEY, DEFAULTIV);

            var str= JsonPase.Serialize(this);

            var data= encrypted.CryptStr(str);

            return data;
        }

        public static ResponseView<TokenModel>    TryGetTokenModel(string source)
        {
            ResponseView<TokenModel> response = null;
            if (string.IsNullOrWhiteSpace(source))
            {
                response = new ResponseView<TokenModel>("无效用户",false,null);
                return response;
            }
            try
            {
                IEncrypted encrypted = new AesEncrypt(DEFAULTKEY, DEFAULTIV);

                var deEnctrypedStr = encrypted.CryptStr(source);

                var model = JsonPase.Deserialize<TokenModel>(deEnctrypedStr);

                response = new ResponseView<TokenModel>("", true, model);
            }
            catch (Exception ex)
            {
                response = new ResponseView<TokenModel>("非法用户",false,null);
            }
            return response;
        }

    }
}
