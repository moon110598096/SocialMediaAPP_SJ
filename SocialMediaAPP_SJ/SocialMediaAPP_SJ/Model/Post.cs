namespace SocialMediaAPP_SJ.Model
{
    public class Post
    {
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime date { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool Removed { get; set; }
    }
}
