
using Able.Store.Service.IService.Categories;
using System.Collections.Generic;
namespace Able.Store.IService.Categories
{
    public interface ICategoryService
    {
        ResponseView<PagingResultView<CategroyProductView>> GetProducts(CategoryProductRequest request);

        ResponseView<IList<CategoryView>> GetCategories(int size);

        ResponseView<IList<CategoryView>> GetCategories(int? categoryId);


    }
}
