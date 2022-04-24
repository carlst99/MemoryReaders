using System;
using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class Advance
{
    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        reader.Advance(2);
        Assert.Equal(2, reader.Index);

        reader.Advance(3);
        Assert.Equal(5, reader.Index);
    }

    [Fact]
    public void SucceedsWhenAdvancingPastEnd()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        reader.Advance(Constants.DataString.Length + 5);
        Assert.Equal(Constants.DataString.Length, reader.Index);
    }

    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.DefaultSpanReader.Advance(-1)
        );
    }
}
