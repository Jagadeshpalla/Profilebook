using Microsoft.AspNetCore.Mvc;
using VBOOK2.Backend.Data;

namespace VBOOK2.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ProfileBookContext _context;

        public AdminController(ProfileBookContext context)
        {
            _context = context;
        }

        [HttpGet("posts/approved")]
        public IActionResult GetApprovedPosts()
        {
            var approvedPosts = _context.Posts.Where(p => p.Status == "Approved").ToList();
            return Ok(approvedPosts);
        }

        [HttpPost("posts/approve/{postId}")]
        public IActionResult ApprovePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post == null) return NotFound();

            post.Status = "Approved";
            _context.SaveChanges();
            return NoContent(); // 204 No Content
        }

        // Additional admin methods for user management and reports
    }

}
