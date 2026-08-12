namespace fitness.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsCustom { get; set; } = false;
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Exercise> Exercises { get; set; } = new();

    }
}