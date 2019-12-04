using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.UsersDomain
{

    [Table("ums_address")]
    public class Address : EntityBase<int>
    {
        [Column("id")]
        public override int Id
        {
            get; set;
        }

        [Column("receiver_id")]
        [ForeignKey("Receiver")]
        [Key]
        public int ReceiverId { get; set; }

        [Required]
        public virtual Receiver Receiver { get; set; }

        [Column("province")]
        public string Province { get; set; }

        [Column("province_code")]
        public string ProvinceCode { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("city_code")]
        public string CityCode { get; set; }

        [Column("area")]
        public string Area { get; set; }

        [Column("area_code")]
        public string AreaCode { get; set; }

        [Column("detail")]
        public string Detail { get; set; }
        [Column("postal")]
        public string Postal { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime
        {
            get; set;
        }

        internal void ChangeData(Address address)
        {
            this.Area = address.Area;
            this.AreaCode = address.AreaCode;
            this.City = address.City;
            this.CityCode = address.CityCode;
            this.Province = address.Province;
            this.ProvinceCode = address.ProvinceCode;
            this.Postal = address.Postal;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
