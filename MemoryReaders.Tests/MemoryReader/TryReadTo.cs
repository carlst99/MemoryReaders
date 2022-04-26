using System;
using System.Linq;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class TryReadTo
{
    [Fact]
    public void SucceedsWithoutAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        bool read = reader.TryReadTo(out ReadOnlyMemory<char> span, Constants.DataString[5], false);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SucceedsWithAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(2);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlyMemory<char> span, Constants.DataString[5], true);
        Assert.True(read);
        Assert.Equal(Constants.DataString[2..5].ToArray(), span.ToArray());
        Assert.Equal(6, reader.Consumed);
    }

    [Fact]
    public void FailsWithoutAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        bool read = reader.TryReadTo(out ReadOnlyMemory<char> span, Constants.AbsentCharacter, false);
        Assert.False(read);
        Assert.Equal(default(ReadOnlyMemory<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsWithAdvancePast()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(1);

        // ReSharper disable once RedundantArgumentDefaultValue
        bool read = reader.TryReadTo(out ReadOnlyMemory<char> span, Constants.AbsentCharacter, true);
        Assert.False(read);
        Assert.Equal(default(ReadOnlyMemory<char>).ToArray(), span.ToArray());
        Assert.Equal(1, reader.Consumed);
    }

    [Fact]
    public void FailsAtEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.Advance(Constants.DataString.Length);

        Assert.False(reader.TryReadTo(out _, Constants.DataString[^1]));
    }
}
