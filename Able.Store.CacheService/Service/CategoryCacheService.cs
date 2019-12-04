using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.IService;
using Able.Store.IService.Categories;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Service.IService.Categories;
using System;
using System.Collections.Generic;
namespace Able.Store.CacheService.Service
{
    public class CategoryCacheService : ICategoryCacheService
    {
        private ICategoryRepository _categoryRepository;
        private Lazy<CacheController> _controller=new Lazy<CacheController>();

        public CategoryCacheService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;


            CacheKeys = new List<string>();

            CacheKeys.Add(Index_Key);
        }
        public string PREFIX {

            get {
                return "category_"; 
            }
        }
        public string Index_Key {

            get {
                return string.Concat(this.PREFIX,"index");
            }
        }
        public IList<string> CacheKeys { get; }

        private int _dbIndex = (int)RedisDbZone.Pms;
        public   IList<CategoryView> GetCategories(int size)
        {
            var data = _controller.Value.HashValues<CategoryView>(Index_Key, _dbIndex);

            if (data == null || data.Count == 0)
            {
                var models = _categoryRepository.GetCategories(size: size);

                data = CategoryView.ToView(models);
            }
            return data;
        }
       
        public  IList<CategoryView> GetCategories(int? categoryId)
        {
            var data =  _controller.Value.HashValues<CategoryView>(Index_Key, _dbIndex);

            if (data == null || data.Count == 0)
            {
                var models = _categoryRepository.GetCategories();

                data = CategoryView.ToView(models);
            }
            return data;
        }
       
    }
}
