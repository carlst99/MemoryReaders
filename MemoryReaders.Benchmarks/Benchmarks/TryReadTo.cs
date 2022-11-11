using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System;
using System.Buffers;

namespace MemoryReaders.Benchmarks.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class TryReadTo
{
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("ShortData")]
    public ReadOnlySpan<char> UsingSequenceReader()
    {
        SequenceReader<char> reader = Constants.GetDefaultSequenceReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5]);
        return span;
    }

    [Benchmark]
    [BenchmarkCategory("ShortData")]
    public ReadOnlyMemory<char> UsingMemoryReader()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.TryReadTo(out ReadOnlyMemory<char> memory, Constants.DataString[5]);
        return memory;
    }

    [Benchmark]
    [BenchmarkCategory("ShortData")]
    public ReadOnlySpan<char> UsingSpanReader()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5]);
        return span;
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("LongData")]
    public ReadOnlySpan<char> UsingLongSequenceReader()
    {
        SequenceReader<char> reader = Constants.GetDefaultLongSequenceReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.LongDataUniqueCharacter);
        return span;
    }

    [Benchmark]
    [BenchmarkCategory("LongData")]
    public ReadOnlyMemory<char> UsingLongMemoryReader()
    {
        MemoryReader<char> reader = Constants.GetDefaultLongMemoryReader();
        reader.TryReadTo(out ReadOnlyMemory<char> memory, Constants.LongDataUniqueCharacter);
        return memory;
    }

    [Benchmark]
    [BenchmarkCategory("LongData")]
    public ReadOnlySpan<char> UsingLongSpanReader()
    {
        SpanReader<char> reader = Constants.GetDefaultLongSpanReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.LongDataUniqueCharacter);
        return span;
    }
}
