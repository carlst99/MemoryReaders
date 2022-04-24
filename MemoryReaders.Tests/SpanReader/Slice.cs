using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class Slice
{
    [Fact]
    public void WithoutStart()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        SpanReader<char> slice = reader.Slice(4);
        Assert.Equal(4, slice.Remaining);
        Assert.Equal(reader.Span[2], slice.Span[0]);
        Assert.Equal(reader.Span[5], slice.Span[3]);
    }

    [Fact]
    public void WithStart()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();

        SpanReader<char> slice = reader.Slice(2, 4);
        Assert.Equal(4, slice.Remaining);
        Assert.Equal(reader.Span[2], slice.Span[0]);
        Assert.Equal(reader.Span[5], slice.Span[3]);
    }
}
