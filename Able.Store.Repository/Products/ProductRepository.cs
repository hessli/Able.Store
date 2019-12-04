using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.Core;
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using Able.Store.Repository.EF;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.Repository.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private IUnitOfWork _unitOfWork;
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Product GetProduct(int id, int skuid)
        {
            var dyProduct = (from e in Entities.Where(x => x.Id == id)
                             select new
                             {
                                 id = e.Id,
                                 e.PropertyName,
                                 PropertyValues = e.Skus.Where(x => x.State == ProductState.上架)
                                               .Select(x => new { x.Id, x.PropertyValue }),
                                 Skus = e.Skus.Where(x => x.State == ProductState.上架 && x.Id == skuid)
                                 .Select(x => new
                                 {
                                     x.Id,
                                     x.Title,
                                     x.PicAndDesc,
                                     x.Content,
                                     x.Sale.Price,
                                     x.Sale.SaleQty,
                                     x.PropertyValue,
                                     x.ProductId,
                                     Attributes = x.Attributes.Select(y => new
                                     {
                                         y.Attribute.Name,
                                         y.AttributeValue.Value
                                     })
                                 }),
                             }
               ).FirstOrDefault();
            Product product = null;
            if (dyProduct != null)
            {
                product = new Product
                {
                    Id = dyProduct.id,
                    PropertyName = dyProduct.PropertyName,
                };
                product.PropertyValues = new List<ProductPropertyValue>();

                foreach (var item in dyProduct.PropertyValues)
                {
                    product.PropertyValues.Add(new ProductPropertyValue
                    {
                        SkuId = item.Id,
                        Value = item.PropertyValue
                    });
                }
                foreach (var item in dyProduct.Skus)
                {
                    product.Skus = new List<Sku>();
                    var sku = new Sku
                    {
                        Content = item.Content,
                        Sale = new SkuSale
                        {
                            SaleQty = item.SaleQty,
                            Price = item.Price
                        },
                        PicAndDesc = item.PicAndDesc,
                        ProductId = item.ProductId,
                        Id = item.Id,
                        PropertyValue = item.PropertyValue,
                        Attributes = new List<SkuAttribute>(),
                        Title = item.Title,
                    };

                    foreach (var subItem in item.Attributes)
                    {
                        sku.Attributes.Add(new SkuAttribute
                        {
                            AttributeValue = new ProductAttributeValue
                            {
                                Value = subItem.Value
                            },
                            Attribute = new ProductAttribute
                            {
                                Name = subItem.Name
                            }
                        });
                    }
                    product.Skus.Add(sku);
                }
            }
            return product;
        }
    }
}
