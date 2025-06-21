
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alefak2.Models;

namespace Alefak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ApiContext _context;

        public LikesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Likes>>> Getlikes()
        {
            return await _context.likes.ToListAsync();
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Likes>> GetLikes(int id)
        {
            var likes = await _context.likes.FindAsync(id);

            if (likes == null)
            {
                return Ok(new List<Likes>());
            }

            return likes;
        }

        // POST: api/Likes/Postlikes/5
        [HttpGet("Postlikes/{PostID}")]
        public async Task<ActionResult<IEnumerable<Likes>>> GetLikespost(int PostID)
        {
            var likes = await _context.likes.Where(p => p.PostID == PostID).ToListAsync();
            if (likes == null || !likes.Any())
                return Ok(new List<Likes>());
            return likes;
        }

        // POST: api/Likes/Countlikes/5
        [HttpGet("Countslikes/{PostID}")]
        public ActionResult<string> CountLikespost(int PostID)
        {
            int likesCount = _context.likes.Where(p => p.PostID == PostID).Count();
            if (likesCount == null)
                return Ok(new List<Likes>());
            return Ok(new Dictionary<string, int> { { "LikesCount", likesCount } });
        }


        // POST: api/Likes
        [HttpPost]
        public async Task<ActionResult<Likes>> PostLikes(Likes likes)
        {
            _context.likes.Add(likes);
            await _context.SaveChangesAsync();

            // Update LikesCount in posts table
            var post = await _context.posts.FindAsync(likes.PostID);
            if (post != null)
            {
                post.LikesCount += 1;
                await _context.SaveChangesAsync();
            }


            return CreatedAtAction("GetLikes", new { id = likes.ID }, likes);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLikes(int id)
        {
            var likes = await _context.likes.FindAsync(id);
            if (likes == null)
            {
                return Ok(new List<Likes>());
            }

            _context.likes.Remove(likes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikesExists(int id)
        {
            return _context.likes.Any(e => e.ID == id);
        }
    }
}
