using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Model.UsersDomain
{
    [Table("ums_user")]
    public class User : EntityBase<int>, IAggregateRoot
    {
        public User()
        {
            ReceiveInfos = new List<Receiver>();
        }
        public User(string wxJwt):this()
        {
             
        }
        [Column("id")]
        [Key]
        public override int Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("pass_word")]
        public string PassWord { get; set; }
        [Column("email")]
        public string Email { get; set; }
        /// <summary>
        /// 微信账号信息
        /// </summary>
        public virtual UserWXAccount WXAccount { get; set; }

        public virtual ICollection<Receiver> ReceiveInfos { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        public Receiver GetPointReceiver(int? receiverId=null)
        {
            Expression<Func<Receiver, bool>> expre = x => true;

            if (receiverId.HasValue && receiverId.Value != default(int))
            {
                expre = expre.And(x=>x.Id==receiverId);
            }
            var entity = ReceiveInfos.FirstOrDefault(expre.Compile());

            return entity;
        }
        public Receiver GetDefault()
        {
            var defaultReceiver = ReceiveInfos
                 .FirstOrDefault(x => x.IsDefault == true);

            return defaultReceiver;
        }
        public void RemoveReciverInfo(int reciverInfoId)
        {
            var addr=ReceiveInfos.FirstOrDefault(x=>x.Id== reciverInfoId);
            if (addr != null)
            {
                ReceiveInfos.Remove(addr);
            }
        }

        public void ModifyReceiverInfo(Receiver reciverInfo)
        {
           var entity= ReceiveInfos.FirstOrDefault(x=>x.Id==reciverInfo.Id);

            if (entity == null)
                throw new ArgumentNullException("指定的收货信息不存在");
              
              entity.ChangeData(reciverInfo);
        }
        public void AddReceiverInfo(Receiver reciverInfo)
        {

            if (this.ReceiveInfos.Count() == 0)
            {
                reciverInfo.IsDefault = true;
            }
            ReceiveInfos.Add(reciverInfo);
        }

        public void SetDefaultReciverInfo(int addressId)
        {
          var reciverInfo = ReceiveInfos.FirstOrDefault(x=>x.Id==addressId);

            if (reciverInfo != null)
            {
                ReceiveInfos.ToList()
                    .ForEach(x=>x.IsDefault=false);

                reciverInfo.IsDefault=true;
            }
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
