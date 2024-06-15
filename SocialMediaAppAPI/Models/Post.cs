using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaAppAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<UserLikedPost> UserLikedPosts { get; set; }
        [NotMapped] // This property is not mapped to the database
        public int UserLikedPostCount { get; set; }
    }
}
