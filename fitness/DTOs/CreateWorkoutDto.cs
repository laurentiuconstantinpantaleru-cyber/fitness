namespace fitness.DTOs
{
    public class CreateWorkoutDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsCustom { get; set; } = true;
        public List<CreateExerciseDto> Exercises { get; set; } = new();
    }
}