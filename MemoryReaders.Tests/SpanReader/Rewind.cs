using System;
using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class Rewind
{
    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();

        reader.Advance(2);
        reader.Rewind(1);
        Assert.Equal(1, reader.Consumed);

        reader.Advance(4);
        reader.Rewind(2);
        Assert.Equal(3, reader.Consumed);
    }

    [Fact]
    public void SucceedsWhenRewindingPastStart()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();

        reader.Advance(5);
        reader.Rewind(10);
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultSpanReader().Rewind(-1)
        );
    }
}
