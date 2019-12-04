
using Able.Store.Infrastructure.Domain;

namespace Able.Store.Adminstrator.Model.UsersDomain
{
    public interface IUserRepository :IRepository<User>
    {
        User GetUserReciverInfos(int userId);

        User GetPointUserWithReceiver(int userId, int receiverId);
    }
}
