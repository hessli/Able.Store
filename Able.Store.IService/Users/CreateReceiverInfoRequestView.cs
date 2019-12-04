using Able.Store.Model.UsersDomain;
using System;

namespace Able.Store.IService.Users
{
    public class CreateReceiverInfoRequestView
    {

        public int receiverId { get; set; }

        public int userid { get; set; }

        public string tel { get; set; }

        public string name { get; set; }


        public string province { get; set; }

        public string provincecode { get; set; }


        public string city { get; set; }


        public string citycode { get; set; }

        public string detailed { get; set; }

        public string area { get; set; }

        public string areacode { get; set; }

        public string postal { get; set; }


        public static Receiver ToReceiver(CreateReceiverInfoRequestView request)
        {
            Receiver receiver = new Receiver
            {
                Id = request.receiverId,

                Address = new Address
                {
                    City = request.city,
                    Area = request.area,
                    CreateTime = DateTime.Now,
                    Detail = request.detailed,
                    AreaCode = request.areacode,
                    CityCode = request.citycode,
                    ProvinceCode = request.provincecode,
                    Province = request.province,
                     Postal=request.postal
                },
                ReceiverName = request.name,
                Tel = request.tel,
                CreateTime = DateTime.Now
            };

            return receiver;
        }


    }
}
