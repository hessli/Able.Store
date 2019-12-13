using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.AdministrativeAreaDomain
{

    [Table("cms_city")]
    public class City : EntityBase<int>
    {
        [Column("id")]
        public override int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

         [Column("province_id")]
         [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<Area> Areas { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [Column("score")]
        public int Score { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
