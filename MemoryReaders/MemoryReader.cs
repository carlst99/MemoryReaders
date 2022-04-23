using System;

namespace MemoryReaders;

/// <summary>
/// Provides methods for reading binary and text data
/// out of a <see cref="ReadOnlyMemory{T}"/> with a focus
/// on performance and minimal or zero heap allocations.
/// </summary>
/// <typeparam name="T">The type of the read-only memory.</typeparam>
public struct MemoryReader<T> where T : unmanaged, IEquatable<T>
{
    private readonly ReadOnlyMemory<T> _data;

    /// <summary>
    /// Gets the current index at which this <see cref="MemoryReader{T}"/>
    /// is consuming data from the read-only memory source.
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Creates a <see cref="MemoryReader{T}"/> over the given <see cref="ReadOnlyMemory{T}"/>
    /// </summary>
    /// <param name="data">The <see cref="ReadOnlyMemory{T}"/> to read.</param>
    public MemoryReader(ReadOnlyMemory<T> data)
    {
        _data = data;
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
        int index = _data.Span[Index..].IndexOf(delimiter);
        if (index == -1)
            return false;

        Index = advancePastDelimiter ? index + 1 : index;
        return true;
    }
}
