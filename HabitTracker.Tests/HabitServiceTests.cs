using HabitTracker.Services;
using HabitTracker.Models;

namespace HabitTracker.Tests
{
    public class HabitServiceTests
    {
        private HabitService service = new HabitService();

        [Fact]
        public void AddHabit_ShouldIncreaseCount()
        {
            int before = service.GetHabits().Count;

            service.AddHabit("Test Habit", "", "General");

            int after = service.GetHabits().Count;

            Assert.True(after > before);
        }

        [Fact]
        public void DeleteHabit_ShouldRemoveHabit()
        {
            service.AddHabit("DeleteMe", "", "General");
            var habit = service.GetHabits().Last();

            service.DeleteHabit(habit.Id);

            Assert.DoesNotContain(service.GetHabits(), h => h.Id == habit.Id);
        }

        [Fact]
        public void CompleteHabit_ShouldMarkComplete()
        {
            service.AddHabit("CompleteMe", "", "General");
            var habit = service.GetHabits().Last();

            service.CompleteHabit(habit.Id);

            var updated = service.GetHabits().First(h => h.Id == habit.Id);

            Assert.True(updated.IsCompletedToday());
        }

        [Fact]
        public void UpdateHabit_ShouldChangeValues()
        {
            service.AddHabit("Old", "", "General");
            var habit = service.GetHabits().Last();

            service.UpdateHabit(habit.Id, "New", "Desc", "Health");

            var updated = service.GetHabits().First(h => h.Id == habit.Id);

            Assert.Equal("New", updated.Name);
            Assert.Equal("Health", updated.Category);
        }

        [Fact]
        public void GetHabits_ShouldReturnList()
        {
            var habits = service.GetHabits();

            Assert.NotNull(habits);
        }
    }
}