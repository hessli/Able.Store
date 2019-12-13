using Able.Store.CommData.Adverts;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.AdvertDomain
{
    [Table("mks_advert")]
    public class Advert : EntityBase<int>, IAggregateRoot
    {
        [Column("id")]
        [Key]
        public override int Id { get; set; }

        [Column("description")]

        public string Description { get; set; }

        [Column("adver_type")]
        [EnumDataType(typeof(AdverType))]
        public AdverType AdverType { get; set; }
        public string TypeName
        {
            get
            {
                return AdverType.ToString();
            }
        }
        public short TypeValue
        {
            get {

                return  (short)AdverType;
            }
        }
        [Column("link")]
        /// <summary>
        /// 跳转路径
        /// </summary>
        public string Link { get; set; }

        [Column("img")]
        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }

        [Column("state")]
        [EnumDataType(typeof(AdvertState))]
        public AdvertState State { get; set; }

        [Column("sort")]
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        public short StateValue
        {

            get
            {
                return (short)State;
            }
        }
        public string StateName
        {

            get
            {
                return State.ToString();
            }
        }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        protected override void Validate()
        {
            
        }
    }
}
