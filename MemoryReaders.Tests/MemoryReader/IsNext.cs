using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class IsNext
{
    [Fact]
    public void SucceedsWithoutAdvancing()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.True(reader.IsNext(Constants.DataString[0]));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void SucceedsAndAdvances()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.True(reader.IsNext(Constants.DataString[0], true));
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancing()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.False(reader.IsNext(Constants.DataString[1]));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAndDoesNotAdvanceWhenEnabled()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.False(reader.IsNext(Constants.DataString[1], true));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);
        Assert.False(reader.IsNext(Constants.DataString[^1]));
    }

    [Fact]
    public void SucceedsWithoutAdvancingSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        Assert.True(reader.IsNext(Constants.DataString[1..3]));
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void SucceedsAndAdvancesSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        Assert.True(reader.IsNext(Constants.DataString[1..3], true));
        Assert.Equal(3, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancingSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.False(reader.IsNext(Constants.DataString[1..2]));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAndDoesNotAdvanceWhenEnabledSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        Assert.False(reader.IsNext(Constants.DataString[1..2], true));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAtEndSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);
        Assert.False(reader.IsNext(Constants.DataString[^3..^1]));
    }
}
