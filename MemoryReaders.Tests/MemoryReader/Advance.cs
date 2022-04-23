using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class Advance
{
    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        reader.Advance(2);
        Assert.Equal(2, reader.Index);

        reader.Advance(3);
        Assert.Equal(5, reader.Index);
    }

    [Fact]
    public void SucceedsWhenAdvancingPastEnd()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        reader.Advance(Constants.DataString.Length + 5);
        Assert.Equal(Constants.DataString.Length, reader.Index);
    }
}
