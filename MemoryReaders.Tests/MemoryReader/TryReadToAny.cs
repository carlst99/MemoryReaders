using System;
using System.Linq;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryReadToAny
{
    [Fact]
    public void SucceedsWithoutAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.DataString[7], Constants.DataString[5], Constants.AbsentCharacter };
        bool read = reader.TryReadToAny(out ReadOnlyMemory<char> span, delimiters, false);

        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SucceedsWithAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        char[] delimiters = { Constants.DataString[7], Constants.DataString[5], Constants.AbsentCharacter };
        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadToAny(out ReadOnlyMemory<char> span, delimiters, true);

        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(6, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        char[] delimiters = { Constants.AbsentCharacter };
        bool read = reader.TryReadToAny(out ReadOnlyMemory<char> span, delimiters, false);

        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        char[] delimiters = { Constants.AbsentCharacter };
        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadToAny(out ReadOnlyMemory<char> span, delimiters, true);

        Assert.False(read);
        Assert.Equal(default(ReadOnlySpan<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);

        char[] delimiters = { Constants.DataString[0] };
        bool read = reader.TryReadToAny(out _, delimiters);
        Assert.False(read);
    }
}
