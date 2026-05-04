using HabitTracker.Models;

namespace HabitTracker.Services
{
    public interface IHabitService
    {
        IReadOnlyList<Habit> GetHabits();
        void AddHabit(string name, string description, string category);
        void CompleteHabit(Guid id);
        void DeleteHabit(Guid id);
        void UpdateHabit(Guid id, string name, string description, string category);
    }
}