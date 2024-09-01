using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPP_SJ.Data;
using SocialMediaAPP_SJ.Model;
using System.Net.Sockets;
using System.Net;

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
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = host.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (user == null)
                return BadRequest("Invalid UserId.");

            comment.Name = user.Name;
            comment.CommentId = Guid.NewGuid().ToString();
            comment.Ip = ipAddress?.ToString();
            comment.Date = DateTime.UtcNow;
            comment.Removed = false;

            _context.comment.Add(comment);
            await _context.SaveChangesAsync();

            var locationUrl = Url.Action("GetCommentById", new { commentId = comment.CommentId });

            return Created(locationUrl, comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            var comment = await _context.comment.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Removed = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{commentId}")]
        private async Task<ActionResult<Comment>> GetCommentById(string commentId)
        {
            var comment = await _context.comment.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }
    }
}
