using System;
using System.Net.Mail;

namespace Able.Store.Infrastructure.Email
{
    public delegate void SendCompleteHandler(object sender, SendCompleteArgs arg);

    public class SendCompleteArgs : EventArgs
    {

        public static readonly int SMTPERRORCODE = 1;
        public static readonly int ARGERRORCODE = 2;
        public static readonly int DISERRORCODE = 3;
        public static readonly int INVALIERRORDCODE = 4;
        public static readonly int FORMARTERRORCODE = 5;
        public string TagName { get; }
        public EmailPackageData Data { get; }
        public bool IsSuccess { get; }
        public int ErrorCode { get; }
        public DateTime SendTime { get; }
        public Exception Ex { get; }
        public SendCompleteArgs(string tagName,EmailPackageData data,
            bool isSuccess=true,
            Exception ex=null)
        {

            this.TagName = tagName;

            this.Data = data;

            this.IsSuccess = isSuccess;

            this.Ex = ex;

           var type=ex.GetType();

            if (type == typeof(ArgumentNullException))
            {
                ErrorCode = ARGERRORCODE;
            }
            else if (type == typeof(ObjectDisposedException))
            {
                ErrorCode = DISERRORCODE;
            }
            else if (type == typeof(InvalidOperationException))
            {
                ErrorCode = INVALIERRORDCODE;
            }
            else if (type == typeof(SmtpException))
            {
                ErrorCode = SMTPERRORCODE;
            }
            else if (type == typeof(FormatException))
            {
                ErrorCode = FORMARTERRORCODE;
            }
        }
    }
}
