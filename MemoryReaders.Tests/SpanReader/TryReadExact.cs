using System;
using System.Linq;
using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryReadExact
{
    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultSpanReader().TryReadExact(out _, -1)
        );
    }

    [Fact]
    public void FailsWithCountLargerThanRemaining()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length - 2);

        Assert.False(reader.TryReadExact(out _, 3));
    }

    [Fact]
    public void Succeeds()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length - 2);

        bool read = reader.TryReadExact(out ReadOnlySpan<char> span, 2);
        Assert.True(read);
        Assert.Equal(Constants.DataString[^2..].ToArray(), span.ToArray());
        Assert.Equal(Constants.DataString.Length, reader.Consumed);
    }
}
