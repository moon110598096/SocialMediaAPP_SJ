using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaAPP_SJ.Model
{
    public class Comment
    {
        [Key]
        [Column("comment_id")]
        public String CommentId { get; set; }
        [Column("name")]
        public String Name {  get; set; }
        [Column("message")]
        public String Message {  get; set; }
        [Column("ip")]
        public String Ip { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("removed")]
        public bool Removed { get; set; }
        [Column("user_id")]
        public String UserId { get; set; }
        [Column("post_id")]
        public String PostId { get; set; } 
    }
}
