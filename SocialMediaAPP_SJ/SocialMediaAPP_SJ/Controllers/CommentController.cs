using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPP_SJ.Data;
using SocialMediaAPP_SJ.Model;

namespace SocialMediaAPP_SJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
        {
            var user = await _context.user.Where(u => u.UserId == comment.UserId).FirstOrDefaultAsync();

            if (user == null)
                return BadRequest("Invalid UserId.");

            comment.Name = user.Name;
            comment.CommentId = Guid.NewGuid().ToString();
            comment.Ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            comment.Date = DateTime.UtcNow;
            comment.Removed = false;

            _context.comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            var comment = await _context.comment.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Removed = true; // 标记为已删除
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{commentId}")]
        private async Task<ActionResult<Comment>> GetCommentById(string commentId)
        {
            var comment = await _context.comment.FindAsync(commentId);

            if (comment == null || comment.Removed)
            {
                return NotFound();
            }

            return Ok(comment);
        }
    }
}
