using Microsoft.EntityFrameworkCore;
using SocialMediaAppAPI.Data;
using SocialMediaAppAPI.Dto;
using SocialMediaAppAPI.Models;

namespace SocialMediaAppAPI.Services
{
    public interface IPostService
    {
        Task<ServiceResponse<Post>> CreatePost(PostCreateDTO postDto, int userId);
        Task<ServiceResponse<Post>> GetPostById(int id);
        Task<ServiceResponse<List<Post>>> GetAllPosts();
        Task<ServiceResponse<Post>> LikePost(int postId, int userId); 
    }

    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Post>> CreatePost(PostCreateDTO postDto, int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");

                var post = new Post
                {
                    Title = postDto.Title,
                    Description = postDto.Description,
                    OwnerId = userId
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return new ServiceResponse<Post>(post, "Post created successfully.");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Post>(null, $"Error creating post: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<Post>> GetPostById(int id)
        {
            try
            {
                var post = await _context.Posts
                    .Include(p => p.UserLikedPosts)
                    .FirstOrDefaultAsync(p => p.Id == id);


                if (post == null)
                {
                    return new ServiceResponse<Post>(null, "Post not found.", false);
                }

                post.UserLikedPostCount = post.UserLikedPosts.Count;

                return new ServiceResponse<Post>(post);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Post>(null, $"Error retrieving post: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<List<Post>>> GetAllPosts()
        {
            try
            {
                var posts = await _context.Posts
                    .Include(p => p.UserLikedPosts)
                    .ToListAsync();


                foreach (var post in posts)
                {
                    post.UserLikedPostCount = post.UserLikedPosts.Count;
                }


                return new ServiceResponse<List<Post>>(posts);
            }

            catch (Exception ex)
            {
                return new ServiceResponse<List<Post>>(null, $"Error retrieving posts: {ex.Message}", false);
            }
        }

        public async Task<ServiceResponse<Post>> LikePost(int postId, int userId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
                return new ServiceResponse<Post>(null, "Post Not Found");

            // Check if the user already liked the post
            var alreadyLiked = await _context.UserLikedPosts
                .AnyAsync(ulp => ulp.PostId == postId && ulp.UserId == userId);

            if (alreadyLiked)
            {
                return new ServiceResponse<Post>(null, "You have already liked this post.");
            }

            // Create a new UserLikedPost entry
            var userLikedPost = new UserLikedPost
            {
                PostId = postId,
                UserId = userId
            };

            _context.UserLikedPosts.Add(userLikedPost);

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Post>(post);
        }
    }
}
