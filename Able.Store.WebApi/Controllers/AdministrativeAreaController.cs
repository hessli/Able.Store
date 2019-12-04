using Able.Store.IService;
using Able.Store.IService.Administration;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class AdministrativeAreaController : BaseController
    {
        public IAdministrationService AdministrationService { get; set; }
        public ResponseView<IList<StrativeView>> GetProvince()
        {
             return AdministrationService.GetProvince();
        }
        public ResponseView<IList<StrativeView>> GetCities(string code)
        {
            return AdministrationService.GetCities(code);
        }
        public ResponseView<IList<StrativeView>> GetAreas(string code)
        {
            return AdministrationService.GetAreas(code);
        }
        
            
    }
}
