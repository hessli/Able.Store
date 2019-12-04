using Able.Store.IService;
using Able.Store.IService.Categories;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Service.IService;
using Able.Store.Service.IService.Categories;
using System.Collections.Generic;
namespace Able.Store.Service.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ICategoryCacheService  _catecoryCacheService;
        public CategoryService(ICategoryRepository categoryRepository,
            ICategoryCacheService categoryCacheService)
        {
            _catecoryCacheService = categoryCacheService;
            _categoryRepository = categoryRepository;


        }
        public ResponseView<IList<CategoryView>> GetCategories(int size)
        {
            var data= _catecoryCacheService.GetCategories(size);

            return base.OutPutResponseView(data);
        }

        public ResponseView<IList<CategoryView>> GetCategories(int? categoryId)
        {
            var data= _catecoryCacheService.GetCategories(categoryId);

            return base.OutPutResponseView(data);
        }

        public ResponseView<PagingResultView<CategroyProductView>> GetProducts(CategoryProductRequest request)
        {
            ResponseView<PagingResultView<CategroyProductView>> data = null;

            if (request.categoryId == default(int))
            {
                data = base.OutPutResponseView(default(PagingResultView<CategroyProductView>), false, "参数异常");

                return data;
            }

            var entity = _categoryRepository.GetFirstOrDefault(x => x.Id == request.categoryId);

            if (entity == null)
            {
                data = base.OutPutResponseView(default(PagingResultView<CategroyProductView>), false, "指定的分类不存在");

                return data;
            }
            var skus = entity.PagingResult(request.page_index, request.page_size);

            var results = CategroyProductView.ToViews(skus);

            return base.OutPutBrokenResponseView(results);
        }
    }
}
