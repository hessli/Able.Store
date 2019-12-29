using System.Collections.Generic;
using System.Text;
namespace Able.Store.Infrastructure.Email
{
    public abstract class AbstractSenderData
    {
         public IEnumerable<string> Tos { get; }
         public string Body { get; }
         public string Subject { get; }

        public string From { get; }
         public Encoding SubjectEncoding { get; }
         public Encoding BodyEncoding { get; }
        protected static readonly Encoding DEFAULTSUBJECT = Encoding.UTF8;
        protected static readonly Encoding DEFAULTBODY = Encoding.UTF8;
        public AbstractSenderData(string from, HashSet<string> tos, string body, string subject)
            :this(from, tos, body,subject, DEFAULTSUBJECT, DEFAULTBODY)
        {


        }
        public AbstractSenderData(string from, HashSet<string> tos,string body,string subject,
            Encoding subjectEncoding,Encoding bodyEncoding)
        {
            this.Tos = tos;
            this.Body = body;
            this.From = from;
            this.Subject = subject;
            this.SubjectEncoding = subjectEncoding;
            this.BodyEncoding = bodyEncoding;
        }
    }
}