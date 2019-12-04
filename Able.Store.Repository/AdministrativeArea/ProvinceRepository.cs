using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.AdministrativeAreaDomain;
using Able.Store.Repository.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Able.Store.Repository.AdministrativeArea
{
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
        public IList<Province> GetProvinces()
        {
              var results=  Entities.Include(x=>x.Cities)
                .Include("Cities.Areas")
                .ToList();

            return results;
        }
    }
}
