using System.Collections.Generic;
namespace Able.Store.IService.Adverts
{
    public interface IAdvertService
    {

        ResponseView<IList<BannerView>> GetBanners(int size);
    }
}
