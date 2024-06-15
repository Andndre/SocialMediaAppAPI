namespace SocialMediaAppAPI.Dto
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class LoginDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
