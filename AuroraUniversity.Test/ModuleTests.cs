namespace AuroraUniversity.Tests;

public class ModuleTests
{
    [Fact]
    public void Constructor_Valid_SetsProperties()
    {
        var m = new Module("CS101", "Intro", "Dr X", 2);
        Assert.Equal("CS101", m.Code);
        Assert.Equal("Intro", m.Title);
        Assert.Equal("Dr X", m.ResponsibleStaff);
        Assert.Equal(2, m.Capacity);
        Assert.Empty(m.Students);
    }

    [Fact]
    public void Constructor_InvalidCode_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Module("AB", "T", "S", 1));
    }

    [Fact]
    public void Constructor_InvalidCapacity_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Module("CS101", "T", "S", 0));
    }

    [Fact]
    public void Enroll_AddsStudent_WhenCapacityAvailable()
    {
        var mod = new Module("CS101", "Intro", "Dr X", 2);
        var s = new Student("Bob");

        var first = mod.Enroll(s);

        Assert.True(first);
        Assert.Contains(s, mod.Students);
        Assert.Contains(mod, s.EnrolledModules);
    }

    [Fact]
    public void Enroll_DoesNotDuplicate_WhenCalledTwice()
    {
        var mod = new Module("CS101", "Intro", "Dr X", 2);
        var s = new Student("Bob");

        Assert.True(mod.Enroll(s));
        Assert.True(mod.Enroll(s)); // second call returns true, but should not duplicate
        Assert.Single(mod.Students);
    }

    [Fact]
    public void Enroll_ReturnsFalse_WhenFull()
    {
        var mod = new Module("CS101", "Intro", "Dr X", 1);
        var s1 = new Student("One");
        var s2 = new Student("Two");

        Assert.True(mod.Enroll(s1));
        Assert.False(mod.Enroll(s2));
        Assert.Single(mod.Students);
    }
}