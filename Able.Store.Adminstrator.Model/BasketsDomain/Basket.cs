using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Able.Store.Adminstrator.Model.BasketsDomain
{
    [Table("bms_basket")]
    public class Basket : EntityBase<int>, IAggregateRoot
    {
        public Basket()
        {

        }
        [Column("id")]
        public override int Id { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        private int? _totalQty = default(int?);
        [NotMapped]
        public int Qty
        {
            get
            {
                if (_totalQty == default(int?))
                {
                     _totalQty = BasketItems.Sum(x => x.TotalQty);
                }
                var deletQty = BasketItems
                     .Where(x => x.EntityState == EntityState.删除)
                    .Sum(x => x.TotalQty);

                return _totalQty.GetValueOrDefault() - deletQty;
            }
        }

        private decimal? _totalCost =default(decimal?);
        [NotMapped]
        public decimal Cost
        {
            get
            {
                if (_totalCost == default(decimal?))
                {
                    _totalCost = BasketItems.Sum(x => x.TotalCost);
                }

                var deletCost=BasketItems
                      .Where(x => x.EntityState == EntityState.删除)
                     .Sum(x=>x.TotalCost);
                return _totalCost.GetValueOrDefault() - deletCost;
            }
        }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public bool Contains(Sku sku)
        {
            if (BasketItems != null)
            {
                return  BasketItems.Any(x=>x.SkuId==sku.Id);
            }
            return false;
        }
        public void RemoveItem(BasketItem  item)
        {
            if (item != null)
                item.EntityState = EntityState.删除;
        }
        public void ChangeItemQty(int skuId,int qty)
        {
            var basketItem= BasketItems
                .FirstOrDefault(x=>x.SkuId==skuId);

            if (basketItem != null)
            {
                basketItem.ChangeQtyTo(qty);
            }
        }

        public void AddItem(Sku sku)
        {
            if (BasketItems == null)
                BasketItems = new List<BasketItem>();
            if (!Contains(sku))
            {
                var item = BasketItemFactory.CrateItemFor(sku, this);
                BasketItems.Add(item);
            }
        }
        public void AddItems(IList<Sku> skus)
        {
            foreach (var item in skus)
            {
                AddItem(item);
            }
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }

    }
}
