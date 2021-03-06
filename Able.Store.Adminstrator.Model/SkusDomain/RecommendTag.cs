﻿using Able.Store.Adminstrator.Model.Core;
using Able.Store.Infrastructure.Domain;

namespace Able.Store.Adminstrator.Model.SkusDomain
{
    public class RecommendTag : ValueOjectBase, ISkuTag
    {

        private Sku _sku;
        public RecommendTag(Sku sku)
        {
            _sku = sku;
        }
        public int SkuId { get; set; }
        private string _baseLink;
        public void SetLink(string baseLink)
        {
            _baseLink = baseLink;
        }
        public string Link
        {
            get
            {
                return _baseLink + SkuId;
            }
        }
        public string TagName
        {
            get
            {
                return ProductTag.推荐.ToString();
            }
        }
        public int TagValue
        {
            get
            {
                return (int)ProductTag.推荐;
            }
        }
        protected override void Validate()
        {
           
        }
    }
}
