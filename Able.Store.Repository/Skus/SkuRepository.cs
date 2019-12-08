using Able.Store.Infrastructure.Querying;
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.Core;
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using Able.Store.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Repository.Skus
{
    public class SkuRepository : BaseRepository<Sku>, ISkuRepository
    {
        public SkuRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<Sku> GetNewProducts(int size)
        {
            var products = (from entity in Entities
                            where entity.Tag == ProductTag.新品 &&
                            entity.State == ProductState.上架
                            orderby entity.Tag
                            select new
                            {
                                entity.Sort,
                                entity.Sale.Price,
                                entity.Sale.SaleQty,
                                entity.PicAndDesc,
                                entity.Id,
                                entity.ProductId,
                                entity.State,
                                entity.Tag,
                                entity.Content,
                                entity.Title
                            }
                 ).OrderBy(x => x.Id).Take(size).ToList().Select(x => new Sku
                 {
                     Id = x.Id,
                     ProductId = x.ProductId,
                     Sale = new SkuSale
                     {
                         Price = x.Price,
                         SkuId = x.Id,
                         SaleQty = x.SaleQty
                     },
                     PicAndDesc = x.PicAndDesc,
                     Content = x.Content,
                     State = x.State,
                     Tag = x.Tag,
                     Title = x.Title
                 }).ToList();
            return products;
        }
        public PagingResult<Sku> PagingResult(string title, IList<OrderByClause> orderParamters,
                              int pageIndex, int pageSize)
        {
            Expression<Func<Sku, bool>> expre = x => x.State == ProductState.上架;

            if (!string.IsNullOrEmpty(title))
            {
                expre = expre.And(x => x.Title.Contains(title));
            }


            var paging = (from entity in Entities.Where(expre)
                          let sale = entity.Sale
                          where sale.SkuId == entity.Id
                          orderby entity.Tag
                          select new
                          {
                              entity.PicAndDesc,
                              entity.Id,
                              entity.ProductId,
                              entity.Tag,
                              entity.PublishTime,
                              sale.SaleQty,
                              sale.Price,
                              entity.Title
                          }
         ).Order(orderParamters).Pagination(pageIndex, pageSize);

            var pageSet = paging.Result.Select(x => new Sku
            {
                PicAndDesc = x.PicAndDesc,
                Id = x.Id,
                Tag = x.Tag,
                Title = x.Title,
                ProductId = x.ProductId,
                Sale = new SkuSale
                {
                    SkuId = x.Id,
                    Price = x.Price,
                    SaleQty = x.SaleQty
                }
            }).ToList();
            PagingResult<Sku> pagingResult = new PagingResult<Sku>(paging.PageCount, pageSet);
            return pagingResult;
        }
        public IList<Sku> GetRecommendProducts(int size)
        {
            var results = (from entity in Entities
                           where entity.Tag == ProductTag.推荐 &&
                            entity.State == ProductState.上架
                           orderby entity.Tag
                           select new
                           {
                               entity.Id,
                               entity.PicAndDesc,
                               entity.Tag,
                               entity.Title,
                           }
              ).Take(size).ToList().Select(x => new Sku
              {
                  Id = x.Id,
                  PicAndDesc = x.PicAndDesc,
                  Tag = x.Tag,
                  Title = x.Title,
              }).ToList();
            return results;
        }
        public IList<Sku> GetSkus(IList<int> skuids)
        {
            var skus = Entities.Where(x => skuids.Contains(x.Id) &&
              x.State == ProductState.上架)
              .Select(x => new
              {
                  x.Id,
                  x.ProductId,
                  x.Product.PropertyName,
                  x.Tag,
                  x.Title,
                  x.PicAndDesc,
                  x.Sale.Price,
                  x.PropertyValue,
                  x.Content,
                  Attributes = x.Attributes.Select(y => new
                  {
                      y.Attribute.Name,
                      y.AttributeValue.Value
                  })

              }).ToList();

            var entities = skus.Select(x => new Sku
            {
                Content = x.Content,
                Id = x.Id,
                Tag = x.Tag,
                Product = new Product
                {
                    Id = x.ProductId,
                    PropertyName = x.PropertyName,
                },
                Sale = new SkuSale
                {
                    Price = x.Price
                },
                PicAndDesc = x.PicAndDesc,
                Title = x.Title,
                PropertyValue = x.PropertyValue,
                Attributes = x.Attributes.Select(y => new SkuAttribute
                {
                    Attribute = new ProductAttribute
                    {
                        Name = y.Name
                    },
                    AttributeValue = new ProductAttributeValue
                    {
                        Value = y.Value
                    }

                }).ToList()
            }).ToList();

            return entities;
        }

    }
}
