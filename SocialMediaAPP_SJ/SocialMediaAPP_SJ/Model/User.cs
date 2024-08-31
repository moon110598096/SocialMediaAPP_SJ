using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaAPP_SJ.Model
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public String UserId {  get; set; }
        [Column("password")]
        public String Password { get; set; }
        [Column("name")]
        public String Name { get; set; }
    }
}