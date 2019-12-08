using Able.Store.IService;
using Able.Store.IService.Users;
using Able.Store.Model.Users;
using Able.Store.Service.IService;
using System.Collections.Generic;

namespace Able.Store.Service.Users
{
    public class UserService:BaseService, IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ResponseView<IList<UserReceiverResponseView>> GetReceiverInfos(int userId)
        {
             var user=_userRepository
                  .GetUserReciverInfos(userId);

            return base.OutPutBrokenResponseView(UserReceiverResponseView.ToView(user));
        }

        public ResponseView<UserReceiverResponseView> GetDefault(int userId)
        {
            var user=_userRepository.GetFirstById(userId);

            var defaultReceiver=user.GetDefault();

            if (defaultReceiver == null)
            {
                return base.OutPutResponseView(default(UserReceiverResponseView), false, "还未设置默认地址");
            }
            var view= UserReceiverResponseView.ToView(defaultReceiver);

            return base.OutPutBrokenResponseView(view);
        }

        public ResponseView SetDefault(int userId, int receiverId)
        {
            var entity=  _userRepository.GetFirstById(userId);

            entity.SetDefaultReciverInfo(receiverId);

            this._userRepository.Commit();

            return base.OutPutSuccessResponseView();
        }

        public ResponseView<int> ChangeReceiverInfo(CreateReceiverInfoRequestView request)
        {
           var entity=  _userRepository.GetFirstById(request.userid);

            if (entity == null)
            {
                return base.OutPutResponseView(default(int),false,"非法用户");
            }

           var receiverInfo=  CreateReceiverInfoRequestView.ToReceiver(request);

            if (request.receiverId != default(int))
                entity.ModifyReceiverInfo(receiverInfo);
             else
               entity.AddReceiverInfo(receiverInfo);
            
            _userRepository.Commit();

            return base.OutPutBrokenResponseView(entity.Id);
        }
    }
}
