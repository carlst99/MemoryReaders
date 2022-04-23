using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryPeek
{
    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        bool peeked = reader.TryPeek(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[0], value);
    }

    [Fact]
    public void SucceedsOnLast()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryPeek(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsAtEnd()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        reader.Advance(Constants.DataString.Length + 1);
        bool peeked = reader.TryPeek(out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }

    [Fact]
    public void SucceedsWithOffset()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        bool peeked = reader.TryPeek(5, out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[5], value);
    }

    [Fact]
    public void SucceedsWithOffsetOnLast()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        reader.Advance(Constants.DataString.Length - 2);
        bool peeked = reader.TryPeek(1, out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsWithOffsetAtEnd()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryPeek(1, out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }
}
