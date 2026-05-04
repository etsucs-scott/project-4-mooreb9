namespace HabitTracker.Models
{
    public class Habit
    {
        public Guid Id { get; private set; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Habit name cannot be empty.");
                _name = value;
            }
        }

        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "General";

        private HashSet<DateTime> _completedDates = new();
        public IReadOnlyCollection<DateTime> CompletedDates => _completedDates;

        public int Streak { get; private set; }

        // Constructor for new habits
        public Habit(string name, string description = "", string category = "General")
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Category = category;
        }

        // Parameterless constructor for JSON
        public Habit() { }

        public bool MarkComplete(DateTime date)
        {
            date = date.Date;

            if (_completedDates.Contains(date))
                return false;

            _completedDates.Add(date);
            UpdateStreak();
            return true;
        }

        public bool IsCompletedOn(DateTime date)
        {
            return _completedDates.Contains(date.Date);
        }

        public bool IsCompletedToday()
        {
            return IsCompletedOn(DateTime.UtcNow);
        }

        public void LoadCompletedDates(IEnumerable<DateTime> dates)
        {
            _completedDates = dates.Select(d => d.Date).ToHashSet();
            UpdateStreak();
        }

        private void UpdateStreak()
        {
            var sorted = _completedDates.OrderByDescending(d => d).ToList();

            int streak = 0;
            DateTime current = DateTime.UtcNow.Date;

            foreach (var date in sorted)
            {
                if (date == current)
                {
                    streak++;
                    current = current.AddDays(-1);
                }
                else if (date < current)
                {
                    break;
                }
            }

            Streak = streak;
        }
    }
}