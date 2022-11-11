using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

namespace MemoryReaders.Benchmarks;

public static class Constants
{
    // Please, do not change this string. You will break many of the benchmarks
    public const string DataString = "The quick brown fox jumped over the lazy dog";
    public const char LongDataUniqueCharacter = '1';

    public static readonly ReadOnlyMemory<char> DataStringMemory;
    public static readonly ReadOnlySequence<char> DataStringSequence;

    public static readonly string LongDataString;
    public static readonly ReadOnlyMemory<char> LongDataStringMemory;
    public static readonly ReadOnlySequence<char> LongDataStringSequence;

    static Constants()
    {
        DataStringMemory = DataString.AsMemory();
        DataStringSequence = new ReadOnlySequence<char>(DataStringMemory);

        StringBuilder sb = new(DataString.Length * 100 + 1);
        for (int i = 0; i < 100; i++)
            sb.Append(DataString);
        sb.Append(LongDataUniqueCharacter);

        LongDataString = sb.ToString();
        LongDataStringMemory = LongDataString.AsMemory();
        LongDataStringSequence = new ReadOnlySequence<char>(LongDataStringMemory);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MemoryReader<char> GetDefaultMemoryReader()
        => new(DataStringMemory);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SequenceReader<char> GetDefaultSequenceReader()
        => new(DataStringSequence);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpanReader<char> GetDefaultSpanReader()
        => new(DataString);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MemoryReader<char> GetDefaultLongMemoryReader()
        => new(LongDataStringMemory);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SequenceReader<char> GetDefaultLongSequenceReader()
        => new(LongDataStringSequence);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpanReader<char> GetDefaultLongSpanReader()
        => new(LongDataString);
}
