using System.Collections.Generic;
namespace Able.Store.IService.Administration
{
    public interface IAdministrationCacheService : IBaseCacheService
    {
        IList<StrativeView> GetProvince();

        IList<StrativeView> GetCities(string parentCode);

        IList<StrativeView> GetAreas(string parentCode);

    }
}
