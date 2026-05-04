namespace HabitTracker.Models
{
    public class HabitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public List<DateTime> CompletedDates { get; set; } = new();
    }
}