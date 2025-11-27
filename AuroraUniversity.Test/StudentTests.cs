using System;
using Xunit;

namespace AuroraUniversity.Tests
{
    public class StudentTests
    {
        [Fact]
        public void Constructor_ValidName_SetsProperties()
        {
            var s = new Student("Alice");
            Assert.Equal("Alice", s.Name);
            Assert.NotEqual(Guid.Empty, s.Id);
            Assert.Empty(s.EnrolledModules);
            Assert.Empty(s.Marks);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Constructor_InvalidName_Throws(string name)
        {
            Assert.Throws<ArgumentException>(() => new Student(name));
        }
    }
}