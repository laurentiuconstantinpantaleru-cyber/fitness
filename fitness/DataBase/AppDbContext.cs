using fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace fitness.DataBase

{
    public class AppDbContext : DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Workout> Workouts { get; set; } = null!;
        public DbSet<Exercise> Exercises { get; set; } = null!;
    }
}
