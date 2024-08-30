using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMediaAPP_SJ.Data;
using SocialMediaAPP_SJ.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaAPP_SJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            var existingUser = await _context.user.FirstOrDefaultAsync(u => u.UserId == newUser.UserId);

            if (existingUser != null)
            {
                return Conflict(new { message = "Account already exists." });
            }

            _context.user.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { userId = newUser.UserId }, new { message = "Registration successful", userId = newUser.UserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var existingUser = await _context.user
                            .FirstOrDefaultAsync(u => u.UserId == loginUser.UserId);

            if (existingUser == null || existingUser.Password != loginUser.Password)
            {
                return Unauthorized(new { message = "Invalid user ID or password." });
            }

            // Create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginUser.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { message = "Login successful", token = tokenString });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful. Please remove the token from client storage." });
        }
    }
 }
