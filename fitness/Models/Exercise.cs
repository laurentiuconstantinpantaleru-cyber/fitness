namespace fitness.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public int Sets { get; set; }                   
        public int Reps { get; set; }                    
        public double Weight { get; set; }               

 
        public int WorkoutId { get; set; }
        public Workout? Workout { get; set; }
    }
}