using System;

namespace MemoryReaders.Tests;

public static class Constants
{
    // Be very careful if changing this string. You might break many tests
    public const string DataString = "The quick brown fox jumped over the lazy dog";
    public const char AbsentCharacter = '1';

    public static MemoryReader<char> DefaultMemoryReader
        => new(DataString.AsMemory());

    public static SpanReader<char> DefaultSpanReader
        => new(DataString);
}
