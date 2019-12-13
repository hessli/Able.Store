using Able.Store.Infrastructure.Domain;
using System.Collections.Generic;

namespace Able.Store.Model.AdministrativeAreaDomain
{
    public  interface IProvinceRepository:IReadOnlyRepository<Province>
    {
         Province  GetProvince(string provinceCode);

         City GetCityArea(string cityCode);
    }
}
