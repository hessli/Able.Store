using Able.Store.IService;
using Able.Store.IService.Adverts;
using Able.Store.Model.AdvertDomain;
using Able.Store.Service.IService;
using System.Collections.Generic;
namespace Able.Store.Service.Adverts
{
    public class AdvertService :BaseService ,IAdvertService
    {
        private IAdvertRepository _advertRepository;
        private IAdverCacheService _cacheService;
        public AdvertService(IAdvertRepository advertRepository, 
            IAdverCacheService cacheService)
        {
            _advertRepository = advertRepository;
            _cacheService = cacheService;
        }
        public  ResponseView<IList<BannerView>> GetBanners(int size)
        {
            var data= _cacheService.GetBanners(size);

            return base.OutPutBrokenResponseView(data);
        }
    }
}
