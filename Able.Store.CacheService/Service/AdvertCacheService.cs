using Able.Store.CommData.Adverts;
using Able.Store.Infrastructure.Cache;
using Able.Store.IService.Adverts;
using Able.Store.Model.AdvertDomain;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.CacheService.Service
{
    public class AdvertCacheService : IAdverCacheService
    {
        private IAdvertRepository _advertRepository;
        private ICacheStorage _cacheStorage;
       
        public AdvertCacheService(IAdvertRepository repository, ICacheStorage cacheStorage)
        {
            _advertRepository = repository;
            _cacheStorage = cacheStorage;
        }

        public IList<BannerView> GetBanners(int size)
        {
           var data=  _cacheStorage.SortedSetRangeByRank<string, BannerView>
                (AdvertCacheKey.DBINDEX, AdvertCacheKey.BANNERKEY,stop:size);

            if (data == null || data.Count == 0)
            {
                var models = _advertRepository
                    .GetList(x => true)
                    .OrderByDescending(x => x.CreateTime).Take(size)
                    .ToList();

                data = BannerView.ToView(models);
            }
            return data;
        }
    }
}
