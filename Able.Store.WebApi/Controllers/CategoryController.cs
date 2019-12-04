using Able.Store.IService;
using Able.Store.IService.Categories;
using Able.Store.Service.IService.Categories;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class CategoryController : BaseController
    {
        public ICategoryService CategoryService { get; set; }

        public ResponseView<IList<CategoryView>> GetCategoriesBy(int id)
        {

             var data=  CategoryService.GetCategories(id);

            return data;
        }

        public ResponseView<PagingResultView<CategroyProductView>> PostCategoryProducts(CategoryProductRequest request)
        {
           var data=  CategoryService.GetProducts(request);

            return data;
        }
        public ResponseView<IList<CategoryView>> GetCategories()
        {
            var data = CategoryService.GetCategories(default(int?));

            return data;
        }
    }
}
