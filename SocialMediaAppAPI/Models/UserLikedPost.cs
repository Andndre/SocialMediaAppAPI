namespace SocialMediaAppAPI.Models
{
    public class UserLikedPost
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
