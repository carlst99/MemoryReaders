using System;
using System.Linq;
using Xunit;

namespace MemoryReaders.Tests.SpanReader;

public class TryReadTo
{
    [Fact]
    public void SucceedsWithoutAdvancePast()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5], false);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SucceedsWithAdvancePast()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5], true);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(6, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancePast()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(1);

        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.AbsentCharacter, false);
        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithAdvancePast()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(1);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.AbsentCharacter, true);
        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryReadTo(out _, Constants.DataString[^1]));
    }

    [Fact]
    public void SucceedsWithoutAdvancePastSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5..7], false);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SucceedsWithAdvancePastSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5..7], true);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(7, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancePastSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(1);

        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, false);
        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithAdvancePastSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(1);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlySpan<char> span, new[] { Constants.AbsentCharacter, Constants.AbsentCharacter }, true);
        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsAtEndSpan()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryReadTo(out _, Constants.DataString[^3..^1]));
    }
}
