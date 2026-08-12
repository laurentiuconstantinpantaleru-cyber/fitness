using fitness.DataBase; 
using fitness.DTOs;
using fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fitness.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

 
    public UsersController(AppDbContext context)
    {
        _context = context;
    }

 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }


    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
           
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); 

        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
}