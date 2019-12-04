using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.BasketsDomain;
using Able.Store.Repository.EF;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.Repository.Baskets
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public void Commit(Basket basket)
        {
            var deleteItems = basket.BasketItems.Where(x => x.EntityState ==
                           Infrastructure.Domain.EntityState.删除).ToList();

            var skus = deleteItems.Select(x => x.BasketSku).ToList();

            var skuAttributes = skus.SelectMany(x => x.BasketSkuAttributes).ToList();

            foreach (var item in skuAttributes)
            {
                base.EFUnitOfWork.Souce<BasketSkuAttribute>().Remove(item);
            }

            foreach (var item in skus)
            {
                base.EFUnitOfWork.Souce<BasketSku>().Remove(item);
            }
            foreach (var item in deleteItems)
            {
                base.EFUnitOfWork.Souce<BasketItem>().Remove(item);
            }
            if (basket.Qty == 0)
            {
                base.Remove(basket);
            }
            base.Commit();
        }
        public Basket GetBasket(int userid, int[] skuids)
        {
            var entity = (from a in Entities
                          where a.UserId == userid
                          orderby a.CreateTime
                          select new
                          {
                              a.UserId,
                              skus = a.BasketItems.Where(x => skuids.Contains(x.SkuId)).Select(z => new
                              {
                                  z.SkuId,
                                  z.BasketSku.Title,
                                  z.BasketSku.PicAndDesc,
                                  z.BasketSku.Price,
                                  z.BasketSku.PropertyName,
                                  z.BasketSku.PropertyValue,
                                  z.TotalCost,
                                  z.TotalQty,
                                  BasketSkuAttributes = z.BasketSku.BasketSkuAttributes.Select(q => new
                                  {
                                      q.Value,
                                      q.Name
                                  })
                              })
                          }
             ).FirstOrDefault();

            if (entity != null)
            {
                Basket basket = new Basket
                {
                    UserId = entity.UserId,

                    BasketItems = new List<BasketItem>(),
                };
                foreach (var item in entity.skus)
                {
                    var basktItem = new BasketItem
                    {
                        TotalCost = item.TotalCost,
                        TotalQty = item.TotalQty,
                        SkuId = item.SkuId,
                        BasketSku = new BasketSku
                        {
                            Title = item.Title,
                            PicAndDesc = item.PicAndDesc,
                            Price = item.Price,
                            PropertyName = item.PropertyName,
                            PropertyValue = item.PropertyValue,
                        }
                    };

                    foreach (var subItem in item.BasketSkuAttributes)
                    {
                        basktItem.BasketSku.BasketSkuAttributes.Add(new BasketSkuAttribute
                        {
                            Name = subItem.Name,
                            Value = subItem.Value
                        });
                    }
                    basket.BasketItems.Add(basktItem);
                }

                return basket;
            }
            return null;
        }
        public Basket GetBasket(int userid)
        {
            var entity = (from a in Entities
                          where a.UserId == userid
                          orderby a.CreateTime
                          select new
                          {
                              a.UserId,
                              skus = a.BasketItems.Select(z => new
                              {
                                  z.SkuId,
                                  z.BasketSku.Title,
                                  z.BasketSku.PicAndDesc,
                                  z.BasketSku.Price,
                                  z.BasketSku.PropertyName,
                                  z.BasketSku.PropertyValue,
                                  z.TotalCost,
                                  z.TotalQty
                              })
                          }
             ).FirstOrDefault();

            if (entity != null)
            {
                Basket basket = new Basket
                {
                    UserId = entity.UserId,
                    BasketItems = new List<BasketItem>(),
                };
                foreach (var item in entity.skus)
                {
                    basket.BasketItems.Add(new BasketItem
                    {
                        TotalCost = item.TotalCost,
                        TotalQty = item.TotalQty,
                        SkuId = item.SkuId,
                        BasketSku = new BasketSku
                        {
                            Title = item.Title,
                            PicAndDesc = item.PicAndDesc,
                            Price = item.Price,
                            PropertyName = item.PropertyName,
                            PropertyValue = item.PropertyValue,
                        }
                    });
                }

                return basket;
            }
            return null;
        }
    }
}
