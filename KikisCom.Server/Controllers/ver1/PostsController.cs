using KikisCom.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Controllers.ver1
{
    [ApiController]
    [Route("kiki/api/v1/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet("check")]
        [Authorize]
        public async Task<IActionResult> CheckMethod()
        {
            return Ok();
        }

        [HttpGet("getPosts")]
        public async Task<IActionResult> GetPosts()
        {
            if (!User.Identity.IsAuthenticated) return StatusCode(401);

            var (result, posts) = await _postService.GetPostsAsync();
            if (result.StatusCode != 200)
            {
                return result;
            }
            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found.");
            }
            return Ok(posts);
        }
    }
}
