using Able.Store.Adminstrator.Model.UsersDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{
    [Table("oms_order_address")]
    public class OrderAddress : ValueOjectBase, IEntityBase<int>
    {
        public OrderAddress()
        { }

        public OrderAddress(Address address)
        {
            this.Area = address.Area;
            this.City = address.City;
            this.Province = address.Province;
            this.AreaCode = address.AreaCode;
            this.CityCode = address.CityCode;
            this.ProvinceCode = address.ProvinceCode;
            this.Detail = address.Detail;
            this.CreateTime = DateTime.Now;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("order_receiver_id")]
        [ForeignKey("OrderReceiver")]
        [Key]
        public int OrderReceiverId { get; set; }

        [Required]
        public virtual OrderReceiver OrderReceiver { get; set; }

        [Column("province_code")]
        public string ProvinceCode { get; set; }

        [Column("province")]
        public string Province { get; set; }

        [Column("city_code")]
        public string CityCode { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("area_code")]
        public string AreaCode { get; set; }
        [Column("area")]
        public string Area { get; set; }

        [Column("detail")]
        public string Detail { get; set; }

        [NotMapped]
        public string DetailAddress
        {
            get {
                return string.Concat(this.Province,this.City,this.Area,this.Detail);
            }
        }

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
