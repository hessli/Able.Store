using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.AdministrativeAreaDomain
{
    [Table("cms_area")]
    public class Area : EntityBase<int>
    {

        [Column("id")]
        public override int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("city_id")]
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

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
