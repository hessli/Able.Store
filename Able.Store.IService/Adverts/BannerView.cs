using Able.Store.Model.AdvertDomain;
using AutoMapper;
using System.Collections.Generic;
namespace Able.Store.IService.Adverts
{
    public class BannerView
    {
        public string link { get; set; }
        public string img { get; set; }
        public static IList<BannerView> ToView(IList<Advert> models)
        {
            Mapper mapper = new Mapper(AutoMapperBootStrapper.Configuration);

            var results = mapper.Map<IList<BannerView>>(models);

            return results;
        }
    }
}
