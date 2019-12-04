using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.IService;
using Able.Store.IService.Adverts;
using Able.Store.Model.AdvertDomain;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.CacheService.Service
{
    public class AdvertCacheService : IAdverCacheService
    {
        private IAdvertRepository _advertRepository;
 
        Lazy<CacheController> _controller  = new Lazy<CacheController>();
        public AdvertCacheService(IAdvertRepository repository)
        {
            _advertRepository = repository;
         
            CacheKeys = new List<string>();
            CacheKeys.Add(IndexBannerKey);
        }
       
        public string PREFIX {

            get {
                return "advert_";
            }
        }
        public string IndexBannerKey {

            get {
                return string.Concat(PREFIX,"index_banner");
            }
        }
        public IList<string> CacheKeys {

            get;private set;
        }
        private int _dbIndex = (int)RedisDbZone.Cms;
        public IList<BannerView> GetBanners(int size)
        {
            var data = _controller.Value
              .HashValues<BannerView>(IndexBannerKey, _dbIndex);

            if (data == null || data.Count == 0)
            {
                var models = _advertRepository
                    .GetList(x => x.AdverType == AdverType.站内广告
                                             && x.State == AdvertState.有效)
                                             .OrderByDescending(x=>x.CreateTime)
                                             .Take(size)
                                             .ToList();
                data = BannerView.ToView(models);
            }
            return data;
        }
    }
}
