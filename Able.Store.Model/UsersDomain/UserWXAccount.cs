using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.UsersDomain
{

    [Table("ums_wx_account")]
    public class UserWXAccount
    {
        public UserWXAccount()
        {

        }
        public UserWXAccount(string jwt)
        {
             
        }
        [Column("user_id")]
        [ForeignKey("User")]
        [Key]
        public int UserId { get; set; }

        [Required]
        public virtual User  User{ get; set; }
        [Column("nick_name")]
         public string NickName { get; set; }
        [Column("gender")]
        public int Gender { get; set; }
        [Column("avatar_url")]
        public string AvatarUrl { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("province")]
        public string Province { get; set; }
        [Column("open_id")]
        public string OpenId { get; set; }
        [Column("mobile")]
        public string Mobile { get; set; }
    }
}
