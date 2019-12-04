using System.Collections.Generic;

namespace Able.Store.IService.Adverts
{
    public interface IAdverCacheService: IBaseCacheService
    {
        IList<BannerView> GetBanners(int size);
    }
}
