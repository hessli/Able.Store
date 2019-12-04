
using Able.Store.Infrastructure.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Able.Store.Model.ProductsDomain
{
    [Table("brand")]
    public class Brand : EntityBase<int>
    {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            
        }
    }
}
