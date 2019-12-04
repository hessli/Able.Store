using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.IService;
using Able.Store.IService.Administration;
using Able.Store.Model.AdministrativeAreaDomain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.CacheService.Service
{
    public class AdministrationCacheService :IAdministrationCacheService
    {
        private Lazy<CacheController> _cacheController=new Lazy<CacheController>();

        private IProvinceRepository _provinceRepository;

        private static object _synch = new object();

        public AdministrationCacheService(IProvinceRepository provinceRepository)
        {
            this.CacheKeys = new List<string>();
            CacheKeys.Add(ProvinceKey);
            CacheKeys.Add(CityKey);
            CacheKeys.Add(AreaKey);

            _provinceRepository = provinceRepository;
        }
        public string PREFIX
        {
            get
            {

                return "administrative_";
            }
        }
        public string ProvinceKey
        {

            get
            {

                return string.Concat(this.PREFIX, "province");
            }
        }

        public string CityKey
        {

            get
            {

                return string.Concat(this.PREFIX, "city");
            }
        }
        public string AreaKey
        {

            get
            {

                return string.Concat(this.PREFIX, "area");
            }
        }
        private int _dbIndex = (int)RedisDbZone.Cms;
        public IList<string> CacheKeys
        {
            get; private set;
        }
        public IList<StrativeView> GetProvince()
        {
            lock (_synch)
            {
                var views = this._cacheController.Value.HashValues<StrativeView>(this.ProvinceKey, _dbIndex);

                if (views == null || views.Count==0)
                {
                    var entities = _provinceRepository.GetProvinces();

                    var tViews = StrativeView.ToView(entities);

                    SetCach(tViews);

                    views = tViews.Item1;
                }

                return views;
            }
        }
        public void SetCach(Tuple<IList<StrativeView>, IList<StrativeView>, IList<StrativeView>>  tuple)
        {

            IList<KeyValuePair<string, StrativeView>> rs = new List<KeyValuePair<string, StrativeView>>();

            foreach (var item in tuple.Item1)
            {
                KeyValuePair<string, StrativeView> keyValuePair = new KeyValuePair<string, StrativeView>(item.code,item);
                rs.Add(keyValuePair);
            }
            _cacheController.Value.HashSet(this.ProvinceKey,rs);

            var items = tuple.Item2;
            var cityDic = items.ToLookup(x => x.parentId, x => x);
            foreach (var item in cityDic)
            {
                _cacheController.Value.HashSet(CityKey, item.Key, item.ToList());
            }
            var areaDic = tuple.Item3.ToLookup(x=>x.parentId,x=>x);
            foreach (var item in areaDic)
            {
                _cacheController.Value.HashSet(AreaKey, item.Key, item.ToList());
            }
        }

        public IList<StrativeView> GetCities(string code)
        {
           var datas = this._cacheController.Value.
                HashSetSan<List<StrativeView>>(this.CityKey,code,_dbIndex);

            return datas;
        }

        public IList<StrativeView> GetAreas(string code)
        {
            var datas = this._cacheController.Value.
                HashSetSan<List<StrativeView>>(this.AreaKey, code, _dbIndex);
            return datas;
        }
    }
}
