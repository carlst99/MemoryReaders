using System;
using System.Linq;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryReadExact
{
    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultMemoryReader().TryReadExact(out _, -1)
        );
    }

    [Fact]
    public void FailsWithCountLargerThanRemaining()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 2);

        Assert.False(reader.TryReadExact(out _, 3));
    }

    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 2);

        bool read = reader.TryReadExact(out ReadOnlyMemory<char> memory, 2);
        Assert.True(read);
        Assert.Equal(Constants.DataString[^2..].ToArray(), memory.ToArray());
        Assert.Equal(Constants.DataString.Length, reader.Consumed);
    }
}
