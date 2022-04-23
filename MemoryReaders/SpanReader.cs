using System;

namespace MemoryReaders;

/// <summary>
/// Provides methods for reading binary and text data
/// out of a <see cref="ReadOnlySpan{T}"/> with a focus
/// on performance and minimal or zero heap allocations.
/// </summary>
/// <typeparam name="T">The type of the read-only span.</typeparam>
public ref struct SpanReader<T> where T : unmanaged, IEquatable<T>
{
    /// <summary>
    /// Gets the underlying <see cref="ReadOnlySpan{T}"/> for the reader.
    /// </summary>
    public readonly ReadOnlySpan<T> Span;

    /// <summary>
    /// Gets the current index at which this <see cref="SpanReader{T}"/>
    /// is consuming data from the read-only span source.
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Creates a <see cref="SpanReader{T}"/> over the given <see cref="ReadOnlySpan{T}"/>
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to read.</param>
    public SpanReader(ReadOnlySpan<T> span)
    {
        Span = span;
        Index = 0;
    }

    /// <summary>
    /// Advances to the given delimiter, if found, and optionally advances past it.
    /// </summary>
    /// <param name="delimiter">The delimiter to search for.</param>
    /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
    /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
    public bool TryAdvanceTo(T delimiter, bool advancePastDelimiter = true)
    {
        int index = Span[Index..].IndexOf(delimiter);
        if (index == -1)
            return false;

        Index = advancePastDelimiter ? index + 1 : index;
        return true;
    }
}
