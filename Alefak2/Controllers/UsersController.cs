using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alefak2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Alefak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Ok(new List<User>());
            }

            return user;
        }
       
        // GET: api/Users/Country/{id}
        [HttpGet("UserName/{id}")]
        public async Task<ActionResult<string>> GetUserName(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Ok(new List<User>());

            }
            var UserName = user.UserName;
            return UserName;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest("User ID mismatch.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return Content("No Users Found. ");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // GET: api/Users/City/{id}
        [HttpGet("City/{id}")]
        public async Task<ActionResult<string>> GetUserCity(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Content("No Users Found. ");
            }
            var city = user.City;
            return city;
        }

        [HttpPut("City/{id}")]
        public async Task<IActionResult> PutCity(int id, User C)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Content("No Users Found. ");
            }

            user.City = C.City; // Only update the Text field

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }

        // GET: api/Users/Country/{id}
        [HttpGet("Country/{id}")]
        public async Task<ActionResult<string>> GetUserCountry(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Content("No Users Found. ");
            }
            var Country = user.Country;
            return Country;
        }

        [HttpPut("Country/{id}")]
        public async Task<IActionResult> PutCountry(int id, User C)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Content("No Users Found. ");
            }

            user.Country = C.Country; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }




      
        // Get: api/User/password/{id}
        [HttpGet("password/{id}")]
        public async Task<ActionResult<string>> GetUserPassowrd(int id)
        {
            var user = await _context.users.FindAsync(id);  

            if (user == null)
            {
                return Ok(new List<User>());
            }
            var pass=user.Password;
            return pass;
        }

        // put: api/User/Password/{id}
        [HttpPut("Password/{id}")]
        public async Task<IActionResult> PutPassword(int id, User pass)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Ok(new List<User>());
            }

            user.Password = pass.Password; // Only update the Text field

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }



        // Post: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }



        // Get: api/User/Email/{id}
        [HttpGet("Email/{id}")]
        public async Task<ActionResult<string>> GetUserEmail(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Ok(new List<User>());
            }
            var mail = user.Email;
            return mail;
        }

        // put: api/User/Email/{id}
        [HttpPut("Email/{id}")]
        public async Task<IActionResult> PutEmail(int id, User mail)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Ok(new List<User>());
            }

            user.Email = mail.Email; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return Ok(new List<User>());

        }



        // Get: api/User/Phone/{id}
        [HttpGet("Phone/{id}")]
        public async Task<ActionResult<string>> GetUserPhone(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return Ok(new List<User>());
            }
            string phone = user.Phone;
            return phone;
        }

        // Put: api/User/Phone/{id}
        [HttpPut("Phone/{id}")]
        public async Task<IActionResult> PutPhone(int id, User ph)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Ok(new List<User>());
            }

            user.Phone = ph.Phone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return Ok(new List<User>());

        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, [FromBody] string userpassword)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return Ok(new List<User>());
            }

            if (string.IsNullOrWhiteSpace(userpassword))
                return BadRequest("Please enter a password.");

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);
            var result = hasher.VerifyHashedPassword(user, user.Password, userpassword);

            if (result == PasswordVerificationResult.Success)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok($"User {user.ID} deleted.");
            }
            else
            {
                return Unauthorized("Wrong password.");
            }
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.ID == id);
        }
    }
}
