using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryAdvanceTo
{
    [Fact]
    public void SuccessfullyAdvancesToDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], false);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 5, reader.Consumed);
    }

    [Fact]
    public void SuccessfullyAdvancesPastDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], true);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 4, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvanceToDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, false);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvancePastDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, true);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryAdvanceTo('a'));
    }

    [Fact]
    public void SuccessfullyAdvancesToSpanDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool advanced = reader.TryAdvanceTo(Constants.DataString[5..10], false);

        Assert.True(advanced);
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SuccessfullyAdvancesPastSpanDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[5..10], true);

        Assert.True(advanced);
        Assert.Equal(10, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvanceToSpanDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool advanced = reader.TryAdvanceTo(new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, false);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvancePastSpanDelimeter()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, true);

        Assert.False(advanced);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void FailsAtEndSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryAdvanceTo(new[] { 'a', 'a' }));
    }
}
