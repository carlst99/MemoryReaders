using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryAdvanceTo
{
    [Fact]
    public void SuccessfullyAdvancesToDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], false);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 5, reader.Consumed);
    }

    [Fact]
    public void SuccessfullyAdvancesPastDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[^5], true);

        Assert.True(advanced);
        Assert.Equal(Constants.DataString.Length - 4, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvanceToDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, false);

        Assert.False(advanced);
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvancePastDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.AbsentCharacter, true);

        Assert.False(advanced);
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryAdvanceTo('a'));
    }

    [Fact]
    public void SuccessfullyAdvancesToSpanDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        bool advanced = reader.TryAdvanceTo(Constants.DataString[5..10], false);

        Assert.True(advanced);
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SuccessfullyAdvancesPastSpanDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(Constants.DataString[5..10], true);

        Assert.True(advanced);
        Assert.Equal(10, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvanceToSpanDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        bool advanced = reader.TryAdvanceTo(new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, false);

        Assert.False(advanced);
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsToAdvancePastSpanDelimeter()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        // ReSharper disable once RedundantArgumentDefaultValue
        bool advanced = reader.TryAdvanceTo(new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, true);

        Assert.False(advanced);
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void FailsAtEndSpan()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryAdvanceTo(new[] { 'a', 'a' }));
    }
}
