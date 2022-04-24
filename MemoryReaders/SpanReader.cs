﻿using System;
using System.Runtime.CompilerServices;

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
    /// is consuming data from the <see cref="Span"/>.
    /// This value will be invalid if the reader has reached the end
    /// of the underlying <see cref="Span"/>.
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the reader has
    /// reached the end of the <see cref="Span"/>.
    /// </summary>
    public readonly bool End => Index >= Span.Length;

    /// <summary>
    /// Gets the number of remaining <typeparamref name="T"/>'s
    /// in the reader's <see cref="Span"/>.
    /// </summary>
    public readonly int Remaining => Span.Length - Index;

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
    /// Advances the reader by the given number of items.
    /// </summary>
    /// <remarks>
    /// If the <paramref name="count"/> would move the reader past the end,
    /// it will simply move to the end of the <see cref="Span"/>.
    /// </remarks>
    /// <param name="count">The number of items to move ahead by.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Advance(int count)
    {
        if (count > Remaining)
            Index = Span.Length;
        else
            Index += count;
    }

    /// <summary>
    /// Peeks at the next value without advancing the reader.
    /// </summary>
    /// <param name="value">The next value, or default if at the <see cref="End"/></param>
    /// <returns><c>False</c> if at the end of the reader.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryPeek(out T value)
    {
        if (End)
        {
            value = default;
            return false;
        }

        value = Span[Index];
        return true;
    }

    /// <summary>
    /// Peeks at the value at the given offset without advancing the reader.
    /// </summary>
    /// <param name="offset">The offset from the current <see cref="Index"/>.</param>
    /// <param name="value">
    /// The value, or default if the offset is beyond the <see cref="End"/> of the reader.
    /// </param>
    /// <returns><c>False</c> if the offset is beyond the <see cref="End"/> of the reader.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryPeek(int offset, out T value)
    {
        if (offset >= Remaining)
        {
            value = default;
            return false;
        }

        value = Span[Index + offset];
        return true;
    }

    /// <summary>
    /// Advances to the given delimiter, if found, and optionally advances past it.
    /// </summary>
    /// <param name="delimiter">The delimiter to search for.</param>
    /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
    /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
    public bool TryAdvanceTo(T delimiter, bool advancePastDelimiter = true)
    {
        if (End)
            return false;

        int index = Span[Index..].IndexOf(delimiter);
        if (index == -1)
            return false;

        Index = advancePastDelimiter ? index + 1 : index;
        return true;
    }

    /// <summary>
    /// Advances to the given delimiter, if found, and optionally advances past it.
    /// </summary>
    /// <param name="delimiter">The delimiter to search for.</param>
    /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
    /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
    public bool TryAdvanceTo(ReadOnlySpan<T> delimiter, bool advancePastDelimiter = true)
    {
        if (End)
            return false;

        int index = Span[Index..].IndexOf(delimiter);
        if (index == -1)
            return false;

        Index = advancePastDelimiter ? index + delimiter.Length : index;
        return true;
    }
}
