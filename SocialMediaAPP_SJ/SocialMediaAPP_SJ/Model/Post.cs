using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaAPP_SJ.Model
{
    public class Post
    {
        [Column("title")]
        public String Title { get; set; }
        [Column("content")]
        public String Content { get; set; }
        [Column("date")]
        public DateTime date { get; set; }
        [Key]
        [Column("post_id")]
        public String PostId { get; set; }
        [Column("user_id")]
        public String UserId { get; set; }
        [Column("removed")]
        public bool Removed { get; set; }
    }
}
