using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class IsNext
{
    [Fact]
    public void SucceedsWithoutAdvancing()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        Assert.True(reader.IsNext(Constants.DataString[0]));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void SucceedsAndAdvances()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        Assert.True(reader.IsNext(Constants.DataString[0], true));
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancing()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        Assert.False(reader.IsNext(Constants.DataString[1]));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAndDoesNotAdvanceWhenEnabled()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        Assert.False(reader.IsNext(Constants.DataString[1], true));
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length);
        Assert.False(reader.IsNext(Constants.DataString[^1]));
    }
}
