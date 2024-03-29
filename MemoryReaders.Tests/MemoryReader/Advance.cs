﻿using System;
using Xunit;

namespace MemoryReaders.Tests.MemoryReader;

public class Advance
{
    [Fact]
    public void Succeeds()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        reader.Advance(2);
        Assert.Equal(2, reader.Consumed);

        reader.Advance(3);
        Assert.Equal(5, reader.Consumed);
    }

    [Fact]
    public void SucceedsWhenAdvancingPastEnd()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        reader.Advance(Constants.DataString.Length + 5);
        Assert.Equal(Constants.DataString.Length, reader.Consumed);
    }

    [Fact]
    public void ThrowsWithNegativeCount()
    {
        Assert.Throws<ArgumentOutOfRangeException>
        (
            () => Constants.GetDefaultMemoryReader().Advance(-1)
        );
    }
}
