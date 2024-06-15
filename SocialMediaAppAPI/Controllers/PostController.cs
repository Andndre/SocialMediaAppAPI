using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAppAPI.Dto;
using SocialMediaAppAPI.Services;
using System.Security.Claims;

namespace SocialMediaAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDTO postDto)
        {
            try
            {
                // Mendapatkan ID pengguna dari token JWT
                int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Panggil service untuk membuat post
                var response = await _postService.CreatePost(postDto, userId);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var response = await _postService.GetPostById(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var response = await _postService.GetAllPosts();
            return Ok(response.Data);
        }

        [HttpGet("{id}/like")]
        public async Task<IActionResult> LikePost(int id)
        {
            int userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _postService.LikePost(id, userId);
            return Ok(response.Data);
        }
    }

}
