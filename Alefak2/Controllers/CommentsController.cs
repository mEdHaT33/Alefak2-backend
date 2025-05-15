using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alefak2.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Alefak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApiContext _context;

        public CommentsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> Getcomments()
        {
            return await _context.comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComments(int id)
        {
            var comments = await _context.comments.FindAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        // POST: api/Likes/PostComments/5
        [HttpGet("PostComments/{postid}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetLikespost(int PostID)
        {
            var comments = await _context.comments.Where(p => p.PostID == PostID).ToListAsync();
            if (comments == null || !comments.Any())
                return NotFound();
            return comments;
        }
        // POST: api/Likes/CountComments/5
        [HttpGet("CountsComments/{postid}")]
        public ActionResult<int> CountLikespost(int PostID)
        {
            int CommentsCount = _context.comments.Where(p => p.PostID == PostID).Count();
            if (CommentsCount == null)
                return NotFound();
            return CommentsCount;
        }
        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditText/{id}")]
        public async Task<IActionResult> PutComments(int id, Comments text)
        {
            var comment = await _context.comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            comment.Text = text.Text; // Only update the Text field

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            _context.comments.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comments.Id }, comments);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments(int id)
        {
            var comments = await _context.comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.comments.Remove(comments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentsExists(int id)
        {
            return _context.comments.Any(e => e.Id == id);
        }
    }
}
