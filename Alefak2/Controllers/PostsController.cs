using Alefak2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alefak2.DTOs;
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

        //git 
        public async Task<ActionResult<IEnumerable<PostWithAuthorDTO>>> GetPosts()
        {
            var posts= await _context.posts.Include(u => u.Author).Select(p => new PostWithAuthorDTO
            {
                ID = p.ID,
                AuthorID = p.AuthorID,
                Username = p.Author.UserName,
                Text = p.Text,
                Date = p.Date,
                Image = p.Image
            }).ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostWithAuthorDTO>> GetPost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpGet("auther/{AuthorID}")]
        public async Task<ActionResult<IEnumerable<PostWithAuthorDTO>>> GetauthPost(int AuthorID)
        {
           var post = await _context.posts.Where(p => p.AuthorID == AuthorID).Include(u=>u.Author).Select(p => new PostWithAuthorDTO
           {
               ID = p.ID,
               AuthorID = p.AuthorID,
               Username = p.Author.UserName,
               Text = p.Text,
               Date = p.Date,
               Image = p.Image
           }).ToListAsync();
            if (post == null || !post.Any())
                return NotFound();
         return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Posts>> CreatePost(Posts post)
        {
            _context.posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.ID }, post);
        }


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
