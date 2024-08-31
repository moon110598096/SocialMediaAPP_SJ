namespace SocialMediaAPP_SJ.Model
{
    public class Comment
    {
        public String CommentId { get; set; }
        public String Name {  get; set; }
        public String Message {  get; set; }
        public String Ip { get; set; }
        public DateTime Date { get; set; }
        public bool Removed { get; set; }
        public String UserId { get; set; }
        public String PostId { get; set; } 
    }
}
