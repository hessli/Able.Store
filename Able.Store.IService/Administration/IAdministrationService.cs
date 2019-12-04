using System.Collections.Generic;

namespace Able.Store.IService.Administration
{
    public interface IAdministrationService
    {
        ResponseView<IList<StrativeView>> GetProvince();

        ResponseView<IList<StrativeView>> GetCities(string parentCode);

        ResponseView<IList<StrativeView>> GetAreas(string parentCode);
    }
}
