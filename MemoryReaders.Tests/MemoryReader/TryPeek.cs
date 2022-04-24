using System;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryPeek
{
    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        bool peeked = reader.TryPeek(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[0], value);
    }

    [Fact]
    public void SucceedsOnLast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryPeek(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length + 1);
        bool peeked = reader.TryPeek(out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }

    [Fact]
    public void SucceedsWithOffset()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        bool peeked = reader.TryPeek(5, out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[5], value);
    }

    [Fact]
    public void SucceedsWithOffsetOnLast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 2);
        bool peeked = reader.TryPeek(1, out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsWithOffsetAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryPeek(1, out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }

    [Fact]
    public void ThrowsWithNegativeOffset()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultMemoryReader().TryPeek(-1, out _)
        );
    }
}
