using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Able.Store.Infrastructure.Email
{
    public class EmailPackageData: AbstractSenderData
    {
        private HashSet<string> _tos;

        public EmailPackageData(string from, HashSet<string> tos, string body, string subject)
            :base(from,tos,body,subject)
        {


        }
        public EmailPackageData(string from, HashSet<string> tos, string body, string subject,
            Encoding subjectEncoding, Encoding bodyEncoding)
            :base(from,tos,body,subject,subjectEncoding,bodyEncoding)
        {

        }
        public uint PackageId
        {
            get;private set;
        }
        /// <summary>
        /// 如果要发送html格式的消息，需要设置这个属性
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;

        public string AttachmentPath { get; set; }
        public Attachment Attachment
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AttachmentPath))
                {
                    var attachment = new Attachment(AttachmentPath);
                    return attachment;
                }
                return null;
            }
        }
        public  MailMessage GetProviderData()
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(From);

            message.Subject = Subject;

            message.SubjectEncoding = SubjectEncoding;

            message.BodyEncoding = BodyEncoding;

            message.Body = Body;

            message.IsBodyHtml = this.IsBodyHtml;

            if (this.Attachment != null)
            {
                message.Attachments.Add(Attachment);
            }

            foreach (var item in _tos)
            {
                message.To.Add(item);
            }
            return message;
        }
    }
}
