using System.Collections.Generic;
namespace Able.Store.Infrastructure.Email
{
    public class EmailDispatch
    {
        public IList<EmailSender> EmailSenders { get; }

        private IEmailRepository _emailRepository;

        private IList<EmailConnection> _connections;
        public EmailDispatch(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;

            EmailSenders = new List<EmailSender>();

            _connections = emailRepository.GetConnections();

            foreach (var item in _connections)
            {
                var sender = new EmailSender(item.TagName, item);

                sender.OnSendComplete += EmailDispatch_OnSendComplete;

                EmailSenders.Add(sender);
            }
        }
        public void Send(EmailPackageData data)
        {
            foreach (var item in EmailSenders)
            {
                var isSuccess = item.Send(data);
                if (isSuccess)
                    break;
            }
        }
        private void EmailDispatch_OnSendComplete(object sender, SendCompleteArgs arg)
        {
            var data = new EmailSendLog(arg.TagName, 
                arg.Data.Tos,
                arg.Data.Body, 
                arg.Data.Subject, 
                arg.Data.From,
                arg.Data.SubjectEncoding,
                arg.Data.BodyEncoding, 
                arg.SendTime,
                arg.ErrorCode, 
                arg.Ex);
            _emailRepository.AddSendLog(data);
        }
    }
}
