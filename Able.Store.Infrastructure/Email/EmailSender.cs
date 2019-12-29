using System;
using System.Net;
using System.Net.Mail;
namespace Able.Store.Infrastructure.Email
{
    public class EmailSender 
    {
        private SmtpClient _smtpClient;
        public string TagName { get; private set; }
        public EmailConnection Option { get; private set; }

        internal event SendCompleteHandler OnSendComplete;
        public EmailSender(string tagName, EmailConnection option)
        {
            this.Option = option;
            TagName = tagName;
        }
        public bool Send(EmailPackageData data)
        {
            try
            {
                Connect();

                var mailMessage = data.GetProviderData();   

                _smtpClient.Send(mailMessage);

                OnSendComplete?.Invoke(this, new SendCompleteArgs(this.TagName, data));

                return true;
            }
            catch (Exception ex)
            {
                OnSendComplete?.Invoke(this, new SendCompleteArgs(this.TagName, data, false, ex));

                return false;
            }
            finally
            {
                this.CloseConnect();
            }
        }
        private void Connect()
        {
            _smtpClient = new SmtpClient();

            _smtpClient.Port = Option.Port;

            _smtpClient.Host = Option.Host;

            _smtpClient.Credentials = new NetworkCredential(Option.Account, Option.PassWord);

            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        }
        public void CloseConnect()
        {
            if (_smtpClient != null)
            {
                _smtpClient.Dispose();

                _smtpClient = null;
            }
        }
       
      
    }
}
