using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class Slice
{
    [Fact]
    public void WithoutStart()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;
        reader.Advance(2);

        MemoryReader<char> slice = reader.Slice(4);
        Assert.Equal(4, slice.Remaining);
        Assert.Equal(reader.Memory.Span[2], slice.Memory.Span[0]);
        Assert.Equal(reader.Memory.Span[5], slice.Memory.Span[3]);
    }

    [Fact]
    public void WithStart()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        MemoryReader<char> slice = reader.Slice(2, 4);
        Assert.Equal(4, slice.Remaining);
        Assert.Equal(reader.Memory.Span[2], slice.Memory.Span[0]);
        Assert.Equal(reader.Memory.Span[5], slice.Memory.Span[3]);
        Assert.Equal(reader.Memory.Span[5], slice.Memory.Span[3]);
    }
}
