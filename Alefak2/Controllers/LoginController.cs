using Alefak2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alefak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ApiContext _context;

        public LoginController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckLogin([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required.");

            var user = await _context.users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);
            var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);


            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user); // Your method to create JWT
            return Ok(new { message = "Login successful",user, token });
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("uid", user.ID.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A3Z$7x1tQp#9LmfK@Jk28z!WbRtXcvnM"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapi.com",
                audience: "yourapi.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
