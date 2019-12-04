using Able.Store.Model.UsersDomain;
using System.Collections.Generic;
namespace Able.Store.IService.Users
{
    public class UserReceiverResponseView
    {
        public int receiverId { get; set; }

        public string name { get; set; }

        public string tel { get; set; }

        public string province { get; set; }
        public string city { get; set; }

        public bool isDefault { get; set; }

        public string area { get; set; }

        public string detailed { get; set; }

        public string postal { get; set; }
        public static UserReceiverResponseView ToView(Receiver receiver)
        {
            var view = new UserReceiverResponseView
            {
                city = receiver.Address.City,
                area = receiver.Address.Area,
                detailed = receiver.Address.Detail,
                name = receiver.ReceiverName,
                province = receiver.Address.Province,
                receiverId = receiver.Id,
                tel = receiver.Tel,
                isDefault = receiver.IsDefault,
                 postal=receiver.Address.Postal
            };
            return view;
        }

        public static IList<UserReceiverResponseView> ToView(User user)
        {
            IList<UserReceiverResponseView> views = new List<UserReceiverResponseView>();
            foreach (var x in user.ReceiveInfos)
            {
                views.Add(ToView(x));
            }
            return views;
        }
    }
}
