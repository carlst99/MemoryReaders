using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryRead
{
    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        bool read = reader.TryRead(out char value);

        Assert.True(read);
        Assert.Equal(1, reader.Consumed);
        Assert.Equal(Constants.DataString[0], value);
    }

    [Fact]
    public void SucceedsOnLast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length - 1);
        bool peeked = reader.TryRead(out char value);

        Assert.True(peeked);
        Assert.Equal(Constants.DataString.Length, reader.Consumed);
        Assert.Equal(Constants.DataString[^1], value);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length + 1);
        bool peeked = reader.TryRead(out char value);

        Assert.False(peeked);
        Assert.Equal(default, value);
    }
}
