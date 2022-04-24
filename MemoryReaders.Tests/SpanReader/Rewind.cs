using System;
using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class Rewind
{
    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        reader.Advance(2);
        reader.Rewind(1);
        Assert.Equal(1, reader.Index);

        reader.Advance(4);
        reader.Rewind(2);
        Assert.Equal(3, reader.Index);
    }

    [Fact]
    public void SucceedsWhenRewindingPastStart()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        reader.Advance(5);
        reader.Rewind(10);
        Assert.Equal(0, reader.Index);
    }

    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.DefaultSpanReader.Rewind(-1)
        );
    }
}
