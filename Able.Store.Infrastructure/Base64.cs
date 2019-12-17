using System;
using System.Text.RegularExpressions;

namespace Able.Store.Infrastructure
{

    public class GB2312Base64Helper
    {
        private static Base64 base64 = new Base64();
        public static string Base64Decode(string source)
        {
           return   base64.Base64Decode(source);
        }

        public static string Base64EnCode(string source)
        {
            return  base64.Base64EnCode(source);
        }
    }
    public class Base64
    {
        private static readonly System.Text.Encoding DEFAULTENCODING = System.Text.Encoding.GetEncoding("gb2312");
        private System.Text.Encoding _encoding;
        private string pattern=@"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";
        public Base64() : this(DEFAULTENCODING)
        {

        }
        public Base64(System.Text.Encoding encoding)
        {
            if (encoding == null)
                _encoding = DEFAULTENCODING;
            else _encoding = encoding;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Base64Decode(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            var bytes = Convert.FromBase64String(source);

            var data = _encoding.GetString(bytes);  

            return data;
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Base64EnCode(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            byte[] bytes = _encoding.GetBytes(source);

            var data = Convert.ToBase64String(bytes);

            return data;
        }

        public bool IsBase64Str(string source)
        {
            return  Regex.IsMatch(source, this.pattern);
        }
    }
}
