using Org.BouncyCastle.Asn1.Mozilla;

namespace SocialMediaAPP_SJ.Model
{
    public class User
    {
        public int UserId {  get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
    }
}