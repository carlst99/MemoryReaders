using BenchmarkDotNet.Attributes;
using System.Buffers;

namespace MemoryReaders.Benchmarks.Benchmarks;

public class IsNext
{
    [Benchmark]
    public bool UsingSequenceReader()
    {
        SequenceReader<char> reader = Constants.GetDefaultSequenceReader();
        return reader.IsNext('j');
    }

    [Benchmark]
    public bool UsingMemoryReader()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();
        return reader.IsNext('j');
    }

    [Benchmark]
    public bool UsingSpanReader()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();
        return reader.IsNext('j');
    }
}
