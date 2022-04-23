using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace MemoryReaders.Benchmarks;

public static class Constants
{
    // Please, do not change this string. You will break many the benchmarks
    public const string DataString = "The quick brown fox jumped over the lazy dog";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MemoryReader<char> GetDefaultMemoryReader()
        => new(DataString.AsMemory());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SequenceReader<char> GetDefaultSequenceReader()
        => new(new ReadOnlySequence<char>(DataString.AsMemory()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpanReader<char> GetDefaultSpanReader()
        => new(DataString);
}
