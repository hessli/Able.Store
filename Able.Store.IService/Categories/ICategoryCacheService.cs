using Able.Store.Service.IService.Categories;
using System.Collections.Generic;

namespace Able.Store.IService.Categories
{
    public interface ICategoryCacheService:IBaseCacheService
    {
        IList<CategoryView> GetCategories(int size);

        IList<CategoryView> GetCategories(int? categoryId);
    }
}
