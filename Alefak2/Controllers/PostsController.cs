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

         public async Task<ActionResult<IEnumerable<PostWithAuthorDTO>>> GetPosts()
         {
            var posts= await _context.posts.Include(u => u.Author).Select(p => new PostWithAuthorDTO
            {
                ID = p.ID,
                AuthorID = p.AuthorID,
                Username = p.Author.UserName,
                Text = p.Text,
                Date = p.Date,
                Image = p.Image,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
            }).ToListAsync();
            return Ok(posts);
         }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostWithAuthorDTO>> GetPost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null)
                return Ok(new List<Posts>());
            return Ok(post);
        }

        [HttpGet("Sharedposts/{id}")]
        public IActionResult DeepLinkRedirect(int id)
        {
            // Optional: check if post exists
            var postExists = _context.posts.Any(p => p.ID == id);
            if (!postExists)
            {
                return Ok(new List<Posts>());
            }

            // Redirect to deep link the app can handle
            return Redirect($"https://25a7-81-10-3-167.ngrok-free.app/Posts/{id}");
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
                return Ok(new List<Posts>());
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
                return Ok(new List<Posts>());

            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }





}
