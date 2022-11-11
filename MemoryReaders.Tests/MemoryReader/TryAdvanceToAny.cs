using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryAdvanceToAny
{
    [Fact]
    public void SuccessfullyAdvancesToDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.DataString[^1], Constants.DataString[^5], Constants.AbsentCharacter };
        bool advanced = reader.TryAdvanceToAny(delimiters, false);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 5, reader.Consumed);
    }

    [Fact]
    public void SuccessfullyAdvancesPastDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.DataString[^1], Constants.DataString[^5], Constants.AbsentCharacter };
        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceToAny(delimiters, true);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 4, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvanceToDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.AbsentCharacter };
        bool advanced = reader.TryAdvanceToAny(delimiters, false);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvancePastDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.AbsentCharacter };
        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceToAny(delimiters, true);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);

        char[] delimiters = { Constants.DataString[0] };
        Assert.False(reader.TryAdvanceToAny(delimiters));
    }
}
