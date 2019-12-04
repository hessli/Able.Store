using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.UsersDomain
{
     
    [Table("ums_receiver")]
    public class Receiver:EntityBase<int>
    { 
        [Column("id")]
        public override int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("receiver_name")]
        public string ReceiverName { get; set; }

        [Column("tel")]
        public string Tel { get; set; }
        public virtual Address Address { get; set; }

        [Column("is_default")]
        public bool IsDefault { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        internal void ChangeData(Receiver receiver)
        {
            this.Tel = receiver.Tel;
            this.ReceiverName = receiver.ReceiverName;
            this.Address.ChangeData(receiver.Address);
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
