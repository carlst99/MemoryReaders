using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryAdvanceTo
{
    [Fact]
    public void SuccessfullyAdvancesToDelimeter()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], false);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 5, reader.Index);
    }

    [Fact]
    public void SuccessfullyAdvancesPastDelimeter()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], true);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 4, reader.Index);
    }

    [Fact]
    public void FailsToAdvanceToDelimeter()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, false);

        Assert.False(advanced);
        Assert.Equal(0, reader.Index);
    }

    [Fact]
    public void FailsToAdvancePastDelimeter()
    {
        MemoryReader<char> reader = Constants.DefaultMemoryReader;

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, true);

        Assert.False(advanced);
        Assert.Equal(0, reader.Index);
    }
}
