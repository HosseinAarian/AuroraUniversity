namespace AuroraUniversity.Tests;

public class MarkTests
{
    [Fact]
    public void Constructor_NullStudent_Throws()
    {
        var module = new Module("CS101", "Intro", "Dr X", 10);
        Assert.Throws<ArgumentNullException>(() => new Mark(null!, module, 50));
    }

    [Fact]
    public void Constructor_NullModule_Throws()
    {
        var student = new Student("Sam");
        Assert.Throws<ArgumentNullException>(() => new Mark(student, null!, 50));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Constructor_InvalidScore_Throws(int invalidScore)
    {
        var student = new Student("Sam");
        var module = new Module("CS101", "Intro", "Dr X", 10);
        Assert.Throws<ArgumentOutOfRangeException>(() => new Mark(student, module, invalidScore));
    }

    [Fact]
    public void EffectiveScore_NoResit_ReturnsFirstAttempt()
    {
        var s = new Student("S");
        var m = new Module("CS101", "Intro", "Dr X", 10);
        var mark = new Mark(s, m, 60);
        Assert.Equal(60, mark.EffectiveScore);
    }

    [Fact]
    public void RecordResit_Valid_UpdatesResitAndEffectiveScore()
    {
        var s = new Student("S");
        var m = new Module("CS101", "Intro", "Dr X", 10);
        var mark = new Mark(s, m, 60);
        mark.RecordResit(80);
        Assert.Equal(80, mark.ResitAttempt);
        Assert.Equal(80, mark.EffectiveScore);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(200)]
    public void RecordResit_Invalid_Throws(int bad)
    {
        var s = new Student("S");
        var m = new Module("CS101", "Intro", "Dr X", 10);
        var mark = new Mark(s, m, 60);
        Assert.Throws<ArgumentOutOfRangeException>(() => mark.RecordResit(bad));
    }
}