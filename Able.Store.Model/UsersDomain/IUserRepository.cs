using Able.Store.Infrastructure.Domain;
using Able.Store.Model.UsersDomain;

namespace Able.Store.Model.Users
{
    public interface IUserRepository :IRepository<User>
    {
        User GetUserReciverInfos(int userId);

        User GetPointUserWithReceiver(int userId, int receiverId);
    }
}
