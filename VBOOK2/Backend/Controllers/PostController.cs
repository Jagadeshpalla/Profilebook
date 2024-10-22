using Microsoft.AspNetCore.Mvc;
using VBOOK2.Backend.Data;
using VBOOK2.Backend.Models;

namespace VBOOK2.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ProfileBookContext _context;

        public PostController(ProfileBookContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Post> CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return post;
        }
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromForm] Post post, IFormFile postImage)
        {
            if (postImage != null)
            {
                var filePath = Path.Combine("uploads", postImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postImage.CopyToAsync(stream);
                }
                post.PostImage = filePath; // Save the file path
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }


        // Other post-related methods (approve, delete, etc.) go here
    }

}
