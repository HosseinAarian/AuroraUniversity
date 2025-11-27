using System;
using Xunit;

namespace AuroraUniversity.Tests
{
    public class AssessedItemTests
    {
        [Fact]
        public void Constructor_Valid_SetsProperties()
        {
            var s = new Student("Stu");
            var m = new Module("CS101", "Intro", "Dr X", 10);
            var mark = new Mark(s, m, 70);
            var date = DateTime.UtcNow;
            var item = new AssessedItem("Exam", 0.5, date, mark);

            Assert.Equal("Exam", item.Name);
            Assert.Equal(0.5, item.Weight);
            Assert.Equal(date, item.AssessmentDateUtc);
            Assert.Equal(mark, item.Mark);
            Assert.Equal(mark.EffectiveScore, item.EffectiveScore);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        public void Constructor_InvalidWeight_Throws(double w)
        {
            var s = new Student("Stu");
            var m = new Module("CS101", "Intro", "Dr X", 10);
            var mark = new Mark(s, m, 70);
            Assert.Throws<ArgumentException>(() => new AssessedItem("A", w, DateTime.UtcNow, mark));
        }

        [Fact]
        public void Constructor_NullMark_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new AssessedItem("A", 0.2, DateTime.UtcNow, null!));
        }
    }
}