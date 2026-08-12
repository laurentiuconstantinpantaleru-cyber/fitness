namespace fitness.DTOs
{
    public class CreateExerciseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public int WorkoutId { get; set; }
    }
}