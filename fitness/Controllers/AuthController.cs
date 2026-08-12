using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fitness.DataBase;
using fitness.DTOs;
using fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

   
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Un utilizator cu acest email există deja!");
            }

          
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Utilizator înregistrat cu succes!" });
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Email sau parolă incorectă!");
            }

          
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return BadRequest("Email sau parolă incorectă!");
            }

            
            string token = CreateJwtToken(user);

            return Ok(new
            {
                token = token,
                userId = user.Id,
                username = user.Username,
                email = user.Email
            });
        }

        private string CreateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}