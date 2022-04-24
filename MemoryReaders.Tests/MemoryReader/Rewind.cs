using System;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class Rewind
{
    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

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
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        reader.Advance(5);
        reader.Rewind(10);
        Assert.Equal(0, reader.Index);
    }

    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultMemoryReader().Rewind(-1)
        );
    }
}
