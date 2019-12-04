using Able.Store.Infrastructure.Domain;
using System.Collections.Generic;

namespace Able.Store.Model.AdministrativeAreaDomain
{
    public  interface IProvinceRepository:IReadOnlyRepository<Province>
    {
        IList<Province> GetProvinces();
    }
}
