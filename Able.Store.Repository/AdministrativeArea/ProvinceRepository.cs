using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.AdministrativeAreaDomain;
using Able.Store.Repository.EF;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.Repository.AdministrativeArea
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Province GetProvince(string provinceCode)
        {
            var province = base.Entities.Where(x => x.Code == provinceCode)
                 .FirstOrDefault();

            province.Cities.ToList();
            return province;
        }

        public  City GetCityArea(string cityCode)
        {
            var data = (from a in Entities
                        from b in a.Cities
                        where b.Code==cityCode
                        select b
               ).FirstOrDefault();

            data.Areas.ToList();

            return data;
        }
    }
}
