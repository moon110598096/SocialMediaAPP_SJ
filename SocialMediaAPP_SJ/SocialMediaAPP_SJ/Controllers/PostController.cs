using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPP_SJ.Data;
using SocialMediaAPP_SJ.Model;

namespace SocialMediaAPP_SJ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            if (post == null)
                return BadRequest("Invalid post data.");

            post.date = DateTime.Now;
            _context.post.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(String id)
        {
            var post = await _context.post.FindAsync(id);

            if (post == null || post.Removed)
                return NotFound("Post not found or already removed.");

            post.Removed = true;
            await _context.SaveChangesAsync();

            return Ok("Post deleted successfully.");
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(String id, [FromBody] Post updatedPost)
        {
            var post = await _context.post.FindAsync(id);

            if (post == null || post.Removed)
                return NotFound("Post not found or already removed.");

            post.Title = updatedPost.Title ?? post.Title;
            post.Content = updatedPost.Content ?? post.Content;
            post.date = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Post updated successfully.");
        }
    }
}
