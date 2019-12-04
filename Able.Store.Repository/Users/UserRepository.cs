using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.Users;
using Able.Store.Model.UsersDomain;
using Able.Store.Repository.EF;
using System.Linq;

namespace Able.Store.Repository.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetPointUserWithReceiver(int userId, int receiverId)
        {

            var entity = (from a in Entities
                          let b = a.ReceiveInfos.FirstOrDefault(x => x.Id == receiverId)
                          where a.Id == userId
                          select new
                          {
                              a.Id,
                              a.Nick,
                              b.ReceiverName,
                              b.Tel,
                              b.Address
                          }
                          ).FirstOrDefault();
            User user = new User
            {
                Id = entity.Id,
                Nick = entity.Nick,
            };

            user.ReceiveInfos.Add(new Receiver
            {
                Tel = entity.Tel,
                ReceiverName = entity.ReceiverName,
                Address = new Address
                {
                    Area = entity.Address.Area,
                    AreaCode = entity.Address.AreaCode,
                    City = entity.Address.City,
                    CityCode = entity.Address.CityCode,
                    ProvinceCode = entity.Address.ProvinceCode,
                    Detail = entity.Address.Detail,
                    Province = entity.Address.Province,
                    Postal = entity.Address.Postal,
                }
            });

            return user;
        }

        public User GetUserReciverInfos(int userId)
        {
            var entity = Entities.Where(x => x.Id == userId).Select(x => new
            {
                x.Id,
                ReciverInfos = x.ReceiveInfos.Select(z => new
                {
                    z.Id,
                    z.IsDefault,
                    z.Tel,
                    z.ReceiverName,
                    z.UserId,
                    z.Address.Province,
                    z.Address.Postal,
                    z.Address.City,
                    z.Address.Area,
                    z.Address.Detail,
                })
            }).FirstOrDefault();

            var model = new User
            {
                Id = entity.Id
            };

            foreach (var item in entity.ReciverInfos)
            {
                model.ReceiveInfos.Add(new Receiver
                {
                    Id = item.Id,
                    IsDefault = item.IsDefault,
                    Tel = item.Tel,
                    UserId = item.UserId,
                    ReceiverName = item.ReceiverName,
                    Address = new Address
                    {
                        City = item.City,
                        Area = item.Area,
                        Postal = item.Postal,
                        Province = item.Province,
                        Detail = item.Detail
                    }
                });
            }
            return model;
        }
    }
}
