using HabitTracker.Models;

namespace HabitTracker.Tests
{
    public class HabitTests
    {
        [Fact]
        public void CreateHabit_ShouldSetName()
        {
            var habit = new Habit("Exercise");
            Assert.Equal("Exercise", habit.Name);
        }

        [Fact]
        public void EmptyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Habit(""));
        }

        [Fact]
        public void MarkComplete_ShouldAddDate()
        {
            var habit = new Habit("Read");
            habit.MarkComplete(DateTime.UtcNow);

            Assert.True(habit.IsCompletedToday());
        }

        [Fact]
        public void MarkComplete_TwiceSameDay_ShouldFail()
        {
            var habit = new Habit("Read");
            habit.MarkComplete(DateTime.UtcNow);

            var result = habit.MarkComplete(DateTime.UtcNow);

            Assert.False(result);
        }

        [Fact]
        public void Streak_ShouldIncreaseConsecutively()
        {
            var habit = new Habit("Workout");

            habit.MarkComplete(DateTime.UtcNow.AddDays(-1));
            habit.MarkComplete(DateTime.UtcNow);

            Assert.Equal(2, habit.Streak);
        }

        [Fact]
        public void Streak_ShouldResetIfGap()
        {
            var habit = new Habit("Workout");

            habit.MarkComplete(DateTime.UtcNow.AddDays(-3));
            habit.MarkComplete(DateTime.UtcNow);

            Assert.Equal(1, habit.Streak);
        }

        [Fact]
        public void IsCompletedOn_ShouldReturnTrue()
        {
            var habit = new Habit("Study");
            var date = DateTime.UtcNow;

            habit.MarkComplete(date);

            Assert.True(habit.IsCompletedOn(date));
        }
    }
}