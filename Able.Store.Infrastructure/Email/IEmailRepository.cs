using System.Collections.Generic;

namespace Able.Store.Infrastructure.Email
{
    public  interface IEmailRepository
    {
        void AddSendLog(EmailSendLog data);
        IList<EmailConnection> GetConnections();
    }
}
