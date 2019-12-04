using Able.Store.Model.SkusDomain;
using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public class SkuAttributeView
    {
        public string name { get; set; }
        public string value { get; set; }
        public static SkuAttributeView ToVIew(SkuAttribute attribute)
        {
            if (attribute != null)
            {
                SkuAttributeView view = new SkuAttributeView
                {
                    name = attribute.Attribute == null ? string.Empty : attribute.Attribute.Name,
                    value = attribute.AttributeValue == null ? string.Empty : attribute.AttributeValue.Value
                };
                return view;
            }
            return null;
        }

        public static IList<SkuAttributeView> ToViews(ICollection<SkuAttribute> attributes)
        {
            IList<SkuAttributeView> results = new List<SkuAttributeView>();

            if (attributes != null && attributes.Count > 0)
            {
                foreach (var item in attributes)
                {
                    results.Add(SkuAttributeView.ToVIew(item));
                }
            }
            return results;
        }
    }
}
