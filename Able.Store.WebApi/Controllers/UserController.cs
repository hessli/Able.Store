using Able.Store.IService;
using Able.Store.IService.Users;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public IUserService UserService { get; set; }
        public ResponseView<int> CreateReceiverInfo(CreateReceiverInfoRequestView request)
        {

            request.userid = 1;
            var data=  UserService.ChangeReceiverInfo(request);
            return data;
        }

        public ResponseView<UserReceiverResponseView> GetDefaultReceiver()
        {
            var data= UserService.GetDefault(1);

            return data;
        }

        public ResponseView PostSetDefault(UserReceiverKeyRequest request)
        {
            var data= UserService.SetDefault(1, request.receiverid);

            return data;
        }

        public ResponseView<IList<UserReceiverResponseView>> GetReceiverInfos()
        {
            var data = UserService.GetReceiverInfos(1);

            return data;
        }

    }
}
