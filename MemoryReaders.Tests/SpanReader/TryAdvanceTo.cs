using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryAdvanceTo
{
    [Fact]
    public void SuccessfullyAdvancesToDelimeter()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], false);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 5, reader.Index);
    }

    [Fact]
    public void SuccessfullyAdvancesPastDelimeter()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], true);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 4, reader.Index);
    }

    [Fact]
    public void FailsToAdvanceToDelimeter()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, false);

        Assert.False(advanced);
        Assert.Equal(0, reader.Index);
    }

    [Fact]
    public void FailsToAdvancePastDelimeter()
    {
        SpanReader<char> reader = Constants.DefaultSpanReader;

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, true);

        Assert.False(advanced);
        Assert.Equal(0, reader.Index);
    }
}
