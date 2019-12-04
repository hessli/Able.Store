using Able.Store.Adminstrator.Model.ProductsDomain;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Querying;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Able.Store.Adminstrator.Model.CategoriesDomain
{

    [Table("pms_category")]
    public class Category : EntityBase<int>, IAggregateRoot
    {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("sort")]
        public int Sort { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public PagingResult<Sku> PagingResult(int pageIndex, int pageSize)
        {
            var result = (from a in Products.AsQueryable()
                          from b in a.Skus
                          orderby a.PublishTime, b.PublishTime descending
                          select b
              ).Pagination(pageIndex, pageSize);

            return result;
        }

        protected override void Validate()
        {

        }
    }
}
