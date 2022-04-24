using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryRead
{
    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        bool read = reader.TryRead(out char value);

        Assert.True(read);
        Assert.Equal(1, reader.Consumed);
        Assert.Equal(Constants.DataString[0], value);
    }

    [Fact]
    public void SucceedsOnLast()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryRead(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString.Length, reader.Consumed);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsAtEnd()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length + 1);
        bool peeked = reader.TryRead(out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }
}
