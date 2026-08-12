using fitness.DataBase;
using fitness.DTOs;
using fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fitness.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExercisesController(AppDbContext context)
        {
            _context = context;
        }

      
        [HttpGet("workout/{workoutId}")]
        public async Task<IActionResult> GetExercisesByWorkout(int workoutId)
        {
            var exercises = await _context.Exercises
                .Where(e => e.WorkoutId == workoutId)
                .ToListAsync();

            return Ok(exercises);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseDto dto)
        {
      
            var workoutExists = await _context.Workouts.AnyAsync(w => w.Id == dto.WorkoutId);
            if (!workoutExists)
            {
                return BadRequest($"Antrenamentul cu ID-ul {dto.WorkoutId} nu există!");
            }

            var exercise = new Exercise
            {
                Name = dto.Name,
                Sets = dto.Sets,
                Reps = dto.Reps,
                Weight = dto.Weight,
                WorkoutId = dto.WorkoutId
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return Ok(exercise);
        }
    }
}