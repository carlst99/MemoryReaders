using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace MemoryReaders.Benchmarks.Benchmarks;

public class TryAdvanceTo
{
    [Benchmark(Baseline = true)]
    public long UsingSequenceReader()
    {
        SequenceReader<char> reader = Constants.GetDefaultSequenceReader();

        reader.TryAdvanceTo('q');
        reader.TryAdvanceTo('b');
        reader.TryAdvanceTo('f');
        reader.TryAdvanceTo('1');
        reader.TryAdvanceTo('j');
        reader.TryAdvanceTo('o');

        return reader.Consumed;
    }

    [Benchmark]
    public int UsingMemoryReader()
    {
        MemoryReader<char> reader = Constants.GetDefaultMemoryReader();

        reader.TryAdvanceTo('q');
        reader.TryAdvanceTo('b');
        reader.TryAdvanceTo('f');
        reader.TryAdvanceTo('1');
        reader.TryAdvanceTo('j');
        reader.TryAdvanceTo('o');

        return reader.Index;
    }

    [Benchmark]
    public int UsingSpanReader()
    {
        SpanReader<char> reader = Constants.GetDefaultSpanReader();

        reader.TryAdvanceTo('q');
        reader.TryAdvanceTo('b');
        reader.TryAdvanceTo('f');
        reader.TryAdvanceTo('1');
        reader.TryAdvanceTo('j');
        reader.TryAdvanceTo('o');

        return reader.Index;
    }
}
