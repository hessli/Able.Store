
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Repository.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
       
        public IList<Category> GetCategories(int? id, int size = 0)
        {
            IList<Category> rs = null;
            Expression<Func<Category, bool>> expre = x => true;
            if (id.HasValue)
            {
                expre = expre.And(x => x.Id == id);
            }

            var query = (from a in Entities.Where(expre)
                         select a
              );

            if (size > 0)
            {
                rs = query.Take(size).ToList();
            }
            else
            {
                rs = query.ToList();
            }

            return rs;
        }
    }
}
