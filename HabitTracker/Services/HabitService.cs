using HabitTracker.Models;
using System.Text.Json;

namespace HabitTracker.Services
{
    public class HabitService : IHabitService
    {
        private List<Habit> habits = new();
        private readonly string filePath = "habits.json";

        public HabitService()
        {
            LoadFromFile();
        }

        public IReadOnlyList<Habit> GetHabits()
        {
            return habits.AsReadOnly();
        }

        public void AddHabit(string name, string description, string category)
        {
            try
            {
                var habit = new Habit(name, description, category);
                habits.Add(habit);
                SaveToFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding habit: {ex.Message}");
            }
        }

        public void CompleteHabit(Guid id)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null)
                throw new InvalidOperationException("Habit not found.");

            if (!habit.MarkComplete(DateTime.UtcNow))
                return;

            SaveToFile();
        }

        public void DeleteHabit(Guid id)
        {
            habits.RemoveAll(h => h.Id == id);
            SaveToFile();
        }

        public void UpdateHabit(Guid id, string name, string description, string category)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null)
                throw new InvalidOperationException("Habit not found.");

            habit.Name = name;
            habit.Description = description;
            habit.Category = category;

            SaveToFile();
        }

        private void SaveToFile()
        {
            try
            {
                var dtoList = habits.Select(h => new HabitDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Description = h.Description,
                    Category = h.Category,
                    CompletedDates = h.CompletedDates.ToList()
                }).ToList();

                var json = JsonSerializer.Serialize(dtoList, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save error: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    habits = new List<Habit>();
                    return;
                }

                var json = File.ReadAllText(filePath);
                var dtoList = JsonSerializer.Deserialize<List<HabitDto>>(json) ?? new();

                habits = dtoList.Select(dto =>
                {
                    var habit = new Habit(dto.Name, dto.Description, dto.Category);
                    typeof(Habit).GetProperty("Id")!.SetValue(habit, dto.Id);
                    habit.LoadCompletedDates(dto.CompletedDates);
                    return habit;
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load error: {ex.Message}");
                habits = new List<Habit>();
            }
        }
    }
}