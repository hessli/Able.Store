using System.Collections.Generic;

namespace Able.Store.IService.Users
{
    public interface IUserService
    {
         /// <summary>
         /// 创建收货信息
         /// </summary>
         /// <returns></returns>
        ResponseView<int> ChangeReceiverInfo(CreateReceiverInfoRequestView request);

        ResponseView<IList<UserReceiverResponseView>> GetReceiverInfos(int userId);

        ResponseView<UserReceiverResponseView> GetDefault(int userId);

        ResponseView SetDefault(int userId, int receiverId);
    }
}
