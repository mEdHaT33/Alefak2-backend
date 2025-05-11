using Alefak2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alefak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApiContext _context;

        public PostsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]

        //git gitgitgitgit
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        {
            return await _context.posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return NotFound();
            return post;
        }

        [HttpGet("auther/{AuthorID}")]
        public async Task<ActionResult<IEnumerable<Posts>>> GetauthPost(int AuthorID)
        {
           var post = await _context.posts.Where(p => p.AuthorID == AuthorID).ToListAsync();
            if (post == null || !post.Any())
                return NotFound();
         return post;
        }

        [HttpPost]
        public async Task<ActionResult<Posts>> CreatePost(Posts post)
        {
            _context.posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.ID }, post);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdatePost(int id, Posts post)
        //{
        //    if (id != post.ID)
        //        return BadRequest();

        //    _context.Entry(post).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return NotFound();

            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }





}
