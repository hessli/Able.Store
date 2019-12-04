using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Domain.Business;
using Able.Store.Model.Core;
namespace Able.Store.Model.SkusDomain
{
    public class NewTag : ValueOjectBase,ISkuTag
    {
        private Sku _sku;
        public NewTag(Sku sku)
        {
            _sku = sku;
            this.SkuId = _sku.Id;
            this.SaleQty = _sku.Sale.SaleQty;
            this.Price = _sku.Sale.Price;
        }
        public int SkuId
        {
            get;set;
        }
        public int SaleQty { get; set; }
        public decimal Price { get; set; }
        public string TagName {
            get {
                return ProductTag.新品.ToString();
            }
        }

        public int TagValue {

            get {
                return (short)ProductTag.新品;
            }
        }
        protected override void Validate()
        {
            if (this.Price == default(decimal))
                base.AddBrokenRule(new BusinessRule("price","新品需设置价格"));
        }
    }
}
