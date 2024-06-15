namespace SocialMediaAppAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<UserLikedPost> UserLikedPosts { get; set; }
    }
}
