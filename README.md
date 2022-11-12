# MemoryReaders

[![Nuget | carlst99.MemoryReaders](https://img.shields.io/nuget/v/carlst99.MemoryReaders?label=carlst99.MemoryReaders)](https://www.nuget.org/packages/carlst99.MemoryReaders)
[![main](https://github.com/carlst99/MemoryReaders/actions/workflows/main.yml/badge.svg)](https://github.com/carlst99/MemoryReaders/actions/workflows/main.yml)
[![codecov](https://codecov.io/gh/carlst99/MemoryReaders/branch/main/graph/badge.svg?token=VA4L8CGX9Q)](https://codecov.io/gh/carlst99/MemoryReaders)

The MemoryReaders package provides `SequenceReader<T>`-like APIs for `Span<T>` and `Memory<T>` instances,
with a similar focus on performance and minimal-or-zero heap allocations.

The `SpanReader<T>` and `MemoryReader<T>` structures afford the ability to read and search `Span/Memory`
instances in a forward-only manner (rewinding *is* supported). 

MemoryReaders targets `netstandard2.1`.

## Install

MemoryReaders is [available on NuGet](https://www.nuget.org/packages/carlst99.MemoryReaders)

> dotnet add package carlst99.MemoryReaders

## Usage

The `SpanReader<T>` can be constructed over any `ReadOnlySpan<T>` instance, and likewise for the `MemoryReader<T>`.

```csharp
const string DataString = "The quick brown fox jumped over the lazy dog";

SpanReader<char> spanReader = new(DataString.AsSpan());
MemoryReader<char> memoryReader = new(DataString.AsMemory());

spanReader.TryReadTo
(
    out ReadOnlySpan<char> readData,
    delimiter: "fox".AsSpan(),
    advancePastDelimiter: false
);

bool foundQ = memoryReader.TryAdvanceTo('q', advancePastDelimiter: true);
if (foundQ && memoryReader.IsNext("uick".AsSpan()))
    Console.WriteLine("That was fast!");
```

## Performance

MemoryReaders is not thoroughly benchmarked, but comparisons have been made between the `SequenceReader`, `SpanReader`
and `MemoryReader` implementations for the `IsNext`, `TryAdvanceTo` and `TryReadTo` methods.

The results of [benchmarking `TryReadTo`](./MemoryReaders.Benchmarks/Benchmarks/TryReadTo.cs) appear typical of the package, and are as follows:

```
// * Summary *

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22621
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.402
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT


|                  Method | Categories |      Mean |    Error |   StdDev | Ratio |
|------------------------ |----------- |----------:|---------:|---------:|------:|
|     UsingSequenceReader |  ShortData |  24.54 ns | 0.097 ns | 0.086 ns |  1.00 |
|       UsingMemoryReader |  ShortData |  17.30 ns | 0.051 ns | 0.042 ns |  0.71 |
|         UsingSpanReader |  ShortData |  15.02 ns | 0.017 ns | 0.015 ns |  0.61 |
|                         |            |           |          |          |       |
| UsingLongSequenceReader |   LongData | 139.89 ns | 0.637 ns | 0.565 ns |  1.00 |
|   UsingLongMemoryReader |   LongData | 131.52 ns | 0.863 ns | 0.721 ns |  0.94 |
|     UsingLongSpanReader |   LongData | 128.04 ns | 0.979 ns | 0.916 ns |  0.91 |
```
