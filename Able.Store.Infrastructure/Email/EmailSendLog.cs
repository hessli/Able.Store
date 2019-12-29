using System;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Email
{
    public class EmailSendLog
    {

        public string TagName { get;  }
        public string Tos { get; }
        public string Body { get; }
        public string Subject { get; }
        public string From { get; }
        public string SubjectEncodingName { get; }
        public string BodyEncodingName { get; }
        public string ExceptionStr { get; }
        public bool IsSuccess { get; }
        public int ErrorCode { get; }
        public DateTime SendTime { get; }

         
        public EmailSendLog(string tagName, IEnumerable<string>  tos,string body,string subject,string from,
            Encoding subjectEncoding,Encoding bodyEncoding,DateTime sendTime, int errorCode, Exception ex)
        {
            this.Tos =Newtonsoft.Json.JsonConvert.SerializeObject(tos);

            this.TagName = tagName;

            this.Body = body;

            this.Subject = subject;

            this.SubjectEncodingName = subjectEncoding.EncodingName;

            this.BodyEncodingName = bodyEncoding.EncodingName;

            this.ErrorCode = errorCode;

            this.IsSuccess = errorCode > 0 ? false : true;

            this.SendTime = sendTime;

            if (ex != null)
            {
                this.ExceptionStr = Newtonsoft.Json.JsonConvert.SerializeObject(ex);
            }
        }
    }
}
