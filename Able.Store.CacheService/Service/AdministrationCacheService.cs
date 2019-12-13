using Able.Store.CommData.AdministrativeAreas;
using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Model;
using Able.Store.IService.Administration;
using Able.Store.Model.AdministrativeAreaDomain;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.CacheService.Service
{
    public class AdministrationCacheService : IAdministrationCacheService
    {

        private ICacheStorage _cacheStorage;
        private IProvinceRepository _provinceRepository;
        private CacheUnitModel _cacheModel;

        public AdministrationCacheService(IProvinceRepository provinceRepository,
            ICacheStorage cacheStorage)
        {
            _provinceRepository = provinceRepository;
            _cacheStorage = cacheStorage;
            _cacheModel = new CacheUnitModel
            {
                CacheStrategy = CacheStrategy.Always,
                DataBaseIndex = AdministrativeAreaCacheKey.DBINDEX
            };
        }

        /// <summary>
        /// 启动压入缓存
        /// </summary>
        public void BootStartInitAdministrationCache()
        {
            SetProvince();
        }
        private void SetProvince()
        {
            var datas = _provinceRepository
           .GetList(x => true, "Cities", "Cities.Areas")
           .OrderBy(x => x.Score)
           .ToList();

            var provinces = StrativeView.ToProvinces(datas);

            IList<KeyValuePair<StrativeView, double>> values =
                new List<KeyValuePair<StrativeView, double>>();
            foreach (var item in provinces)
            {
                values.Add(new KeyValuePair<StrativeView, double>(item, item.score));
            }
            _cacheStorage.SortedSetAdd(_cacheModel,
                AdministrativeAreaCacheKey.PROVINCE, values);

            foreach (var item in datas)
            {
                this.SetCity(item);
            }
        }

        private void SetCity(Province entity)
        {
            IList<KeyValuePair<StrativeView, double>> values =
              new List<KeyValuePair<StrativeView, double>>();

           var cities=  StrativeView.ToCities(entity);

            foreach (var item in cities)
            {
                values.Add(new KeyValuePair<StrativeView, double>(item,item.score));
            }
            _cacheStorage.SortedSetAdd(_cacheModel, entity.Code, values);

            foreach (var item in entity.Cities)
            {
                this.SetArea(item);
            }
        }

        private void SetArea(City city)
        {
            IList<KeyValuePair<StrativeView, double>> values =
           new List<KeyValuePair<StrativeView, double>>();

            var areas = StrativeView.ToArea(city);

            foreach (var item in areas)
            {
                values.Add(new KeyValuePair<StrativeView, double>(item, item.score));
            }
            _cacheStorage.SortedSetAdd(_cacheModel, city.Code, values);
        }

        public IList<StrativeView> GetProvince()
        {
            var data = _cacheStorage.SortedSetRangeByRank<string, StrativeView>
                           (AdministrativeAreaCacheKey.DBINDEX, AdministrativeAreaCacheKey.PROVINCE);

            if (data == null || data.Count == 0)
            {
                var entities = _provinceRepository.GetList(x => true).ToList();

                data = StrativeView.ToProvinces(entities);
            }
            return data;
        }
        public IList<StrativeView> GetCities(string provinceCode)
        {
            var data = _cacheStorage.SortedSetRangeByRank<string, StrativeView>
                          (AdministrativeAreaCacheKey.DBINDEX, provinceCode);

            if (data == null || data.Count == 0)
            {
                var entities = _provinceRepository.GetProvince(provinceCode);

                data = StrativeView.ToCities(entities);
            }
            return data;
        }
        public IList<StrativeView> GetAreas(string cityCode)
        {
            var data = _cacheStorage.SortedSetRangeByRank<string, StrativeView>
                         (AdministrativeAreaCacheKey.DBINDEX, cityCode);

            if (data == null || data.Count == 0)
            {
                var entity = _provinceRepository.GetCityArea(cityCode);

                data = StrativeView.ToArea(entity);
            }
            return data;
        }
    }
}
