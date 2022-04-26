using BenchmarkDotNet.Attributes;
using System;
using System.Buffers;

namespace MemoryReaders.Benchmarks.Benchmarks;

public class TryReadTo
{
    [Benchmark(Baseline = true)]
    public ReadOnlySpan<char> UsingSequenceReader()
    {
        SequenceReader<char> reader = Constants.GetDefaultSequenceReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5]);
        return span;
    }

    [Benchmark]
    public ReadOnlyMemory<char> UsingMemoryReader()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        reader.TryReadTo(out ReadOnlyMemory<char> memory, Constants.DataString[5]);
        return memory;
    }

    [Benchmark]
    public ReadOnlySpan<char> UsingSpanReader()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        reader.TryReadTo(out ReadOnlySpan<char> span, Constants.DataString[5]);
        return span;
    }
}
