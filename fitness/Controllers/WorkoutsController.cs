using System.Security.Claims;
using fitness.DataBase;
using fitness.DTOs;
using fitness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fitness.Controllers
{
    [Authorize] // Protejăm toate rutele
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkoutsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔒 1. Extrage DOAR antrenamentele utilizatorului conectat
        [HttpGet("my-workouts")]
        public async Task<IActionResult> GetMyWorkouts()
        {
            int userId = GetUserIdFromToken();

            var workouts = await _context.Workouts
                .Include(w => w.Exercises)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return Ok(workouts);
        }

        // 🔒 2. Creează un antrenament automat asociat utilizatorului conectat
        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutDto dto)
        {
            int userId = GetUserIdFromToken();

            var workout = new Workout
            {
                Name = dto.Name,
                IsCustom = dto.IsCustom,
                UserId = userId, // 👈 Se atribuie automat user-ului din token!
                Exercises = dto.Exercises.Select(e => new Exercise
                {
                    Name = e.Name,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight
                }).ToList()
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }

        // 💡 Metodă helper privată pentru a citi ID-ul din Claims-urile JWT
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }
    }
}