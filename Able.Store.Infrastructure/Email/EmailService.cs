using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Email
{
    public class EmailService
    {
        private EmailDispatch _dispatch;
        public EmailService()
        {
            _dispatch = new EmailDispatch(new EmailDBRepository());
        }
        public async void SendAsync(EmailPackageData data)
        {
            await Task.Run(()=> {
                _dispatch.Send(data);
            });
        }

        public void Send(EmailPackageData data)
        {
            _dispatch.Send(data);
        }
    }
}
