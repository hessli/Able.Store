using Able.Store.CommData.Categories;
using Able.Store.Infrastructure.Cache;
using Able.Store.IService.Categories;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Service.IService.Categories;
using System.Collections.Generic;
namespace Able.Store.CacheService.Service
{
    public class CategoryCacheService : ICategoryCacheService
    {
        private ICategoryRepository _categoryRepository;
        private ICacheStorage _cacheStorage;
        public CategoryCacheService(ICategoryRepository categoryRepository,ICacheStorage cacheStorage)
        {
            _categoryRepository = categoryRepository;
            _cacheStorage = cacheStorage;
        }
        
        public   IList<CategoryView> GetCategories(int size)
        {
           var data= _cacheStorage.SortedSetRangeByRank<string, CategoryView>
                (CategoryStaticResource.DBINDEX, CategoryStaticResource.INDEX,stop:size);

            if (data == null || data.Count == 0)
            {
                var models = _categoryRepository.GetCategories(size: size);

                data = CategoryView.ToView(models);
            }
            return data;
        }
       
        public  IList<CategoryView> GetCategories(int? categoryId)
        {

            var data = _cacheStorage.SortedSetRangeByRank<string, CategoryView>
               (CategoryStaticResource.DBINDEX, CategoryStaticResource.INDEX);

            if (data == null || data.Count == 0)
            {
                var models = _categoryRepository.GetCategories();

                data = CategoryView.ToView(models);
            }
            return data;
        }
       
    }
}
