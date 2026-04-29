using System.Text.Json;
using HabitTracker.Models;

namespace HabitTracker.Services
{
    public class HabitService
    {
        private List<Habit> habits = new();
        private readonly string filePath = "habits.json";

        public HabitService()
        {
            LoadFromFile();
        }

        public List<Habit> GetHabits()
        {
            return habits;
        }

        public void AddHabit(string name, string description, string category)
        {
            habits.Add(new Habit
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Category = category,
                Streak = 0
            });

            SaveToFile();
        }

        public void CompleteHabit(Guid id)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null) return;

            var today = DateTime.UtcNow.Date;

            // Prevent duplicate completion for the same day
            if (habit.CompletedDates.Any(d => d.Date == today))
                return;

            habit.CompletedDates.Add(today);

            if (habit.CompletedDates.Count == 1)
            {
                habit.Streak = 1;
            }
            else
            {
                var yesterday = today.AddDays(-1);

                if (habit.CompletedDates.Any(d => d.Date == yesterday))
                {
                    habit.Streak++;
                }
                else
                {
                    habit.Streak = 1;
                }
            }

            SaveToFile();
        }

        public void DeleteHabit(Guid id)
        {
            habits.RemoveAll(h => h.Id == id);
            SaveToFile();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(habits, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }

        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                habits = JsonSerializer.Deserialize<List<Habit>>(json) ?? new List<Habit>();

                // Clean + normalize dates
                foreach (var habit in habits)
                {
                    habit.CompletedDates = habit.CompletedDates
                        .Select(d => d.Date)
                        .Distinct()
                        .ToList();
                }
            }
        }
        public void UpdateHabit(Guid id, string name, string description, string category)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null) return;

            habit.Name = name;
            habit.Description = description;
            habit.Category = category;

            SaveToFile();
        }
    }
}