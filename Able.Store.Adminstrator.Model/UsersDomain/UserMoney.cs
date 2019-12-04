using Able.Store.Infrastructure.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.UsersDomain
{


    [Table("user_money")]
    public class UserMoney:IEntityState
    {
        [Column("id")]
         public int Id { get; set; }
        [Column("user_id")]
        public int Userid { get; set; }
        public virtual User User { get; set; }

         [Column("Vla")]
         public string Vla { get; set; }

        [NotMapped]
        public EntityState? EntityState { get; set; }
    }
}
