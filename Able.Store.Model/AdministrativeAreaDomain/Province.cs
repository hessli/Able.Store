using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.AdministrativeAreaDomain
{

    [Table("cms_province")]
   public class Province:EntityBase<int>,IAggregateRoot
    {

        [Column("id")]
        public override int Id { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public  virtual ICollection<City> Cities { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
