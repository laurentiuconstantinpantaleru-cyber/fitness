namespace fitness.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }= string.Empty;
        public bool IsPremium { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Workout> Workouts { get; set; } = new();
    }
}
