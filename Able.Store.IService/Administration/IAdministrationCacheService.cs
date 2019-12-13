using System.Collections.Generic;
namespace Able.Store.IService.Administration
{
    public interface IAdministrationCacheService : IBaseCacheService
    {
        IList<StrativeView>  GetProvince();

        IList<StrativeView> GetCities(string provinceCode);

        IList<StrativeView> GetAreas(string cityCode);

        /// <summary>
        /// 应用程序启动将省份信息加入缓存
        /// </summary>
        void BootStartInitAdministrationCache();

    }
}
