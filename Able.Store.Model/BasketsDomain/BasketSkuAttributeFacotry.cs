using Able.Store.Model.SkusDomain;
using System.Collections.Generic;
namespace Able.Store.Model.BasketsDomain
{
    public static class BasketSkuAttributeFacotry
    {
        public static ICollection<BasketSkuAttribute> CrateAttributeFor(IEnumerable<SkuAttribute> skuAttributes)
        {
            ICollection<BasketSkuAttribute> attributes = new List<BasketSkuAttribute>();

            foreach (var item in skuAttributes)
            {
                BasketSkuAttribute entity = new BasketSkuAttribute
                {
                    Name = item.Attribute.Name,
                    Value = item.AttributeValue.Value
                };

                attributes.Add(entity);
            }

            return attributes;

        }
    }
}
