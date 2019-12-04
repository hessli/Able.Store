using Able.Store.IService;
using Able.Store.IService.Administration;
using Able.Store.Model.AdministrativeAreaDomain;
using Able.Store.Service.IService;
using System.Collections.Generic;

namespace Able.Store.Service.Administration
{
    public class AdministrationService :BaseService, IAdministrationService
    {
        private IProvinceRepository _provinceRepository;
        private IAdministrationCacheService _administrationCacheService;
        public AdministrationService(IProvinceRepository provinceRepository, 
            IAdministrationCacheService administrationCacheService)
        {

            _administrationCacheService = administrationCacheService;
            _provinceRepository = provinceRepository;
        }
        public ResponseView<IList<StrativeView>> GetAreas(string code)
        {
            var data = _administrationCacheService.GetAreas(code);

            return base.OutPutResponseView(data);
                 
        } 

        public ResponseView<IList<StrativeView>> GetCities(string code)
        {

            var data = _administrationCacheService.GetCities(code);
            return base.OutPutResponseView(data);
        }

        public ResponseView<IList<StrativeView>> GetProvince()
        {
            var data = _administrationCacheService.GetProvince();
            return base.OutPutResponseView(data);
        }
    }
}
