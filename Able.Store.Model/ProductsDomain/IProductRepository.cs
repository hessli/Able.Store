using Able.Store.Infrastructure.Domain;

namespace Able.Store.Model.ProductsDomain
{
    public interface IProductRepository:IRepository<Product>
    {
        Product GetProduct(int id,int skuId);
    } 
}
