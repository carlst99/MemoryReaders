﻿using System;
using System.Runtime.CompilerServices;

namespace MemoryReaders
{
    public partial struct MemoryReader<T> where T : unmanaged, IEquatable<T>
    {
        /// <summary>
        /// Checks to see if the given value is next.
        /// </summary>
        /// <param name="value">The value to check for.</param>
        /// <param name="advancePast"><c>True</c> to advance past the value if found.</param>
        /// <returns><c>True</c> if the given value is next.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNext(T value, bool advancePast = false)
        {
            if (End)
                return false;

            if (!Memory.Span[Consumed].Equals(value))
                return false;

            if (advancePast)
                Consumed++;

            return true;
        }

        /// <summary>
        /// Checks to see if the given value is next.
        /// </summary>
        /// <param name="value">The value to check for.</param>
        /// <param name="advancePast"><c>True</c> to advance past the value if found.</param>
        /// <returns><c>True</c> if the given value is next.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNext(ReadOnlySpan<T> value, bool advancePast = false)
        {
            if (End)
                return false;

            if (!Memory.Span[Consumed..].StartsWith(value))
                return false;

            if (advancePast)
                Consumed += value.Length;

            return true;
        }

        /// <summary>
        /// Attempts to read everything up to the given <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="memory">The read data, if any.</param>
        /// <param name="delimiter">The delimiter to search for.</param>
        /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
        /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
        public bool TryReadTo(out ReadOnlyMemory<T> memory, T delimiter, bool advancePastDelimiter = true)
        {
            int currIndex = Consumed;

            if (!TryAdvanceTo(delimiter, false))
            {
                memory = default;
                return false;
            }

            memory = Memory[currIndex..Consumed];
            if (advancePastDelimiter)
                Consumed++;

            return true;
        }

        /// <summary>
        /// Attempts to read everything up to the given <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="memory">The read data, if any.</param>
        /// <param name="delimiter">The delimiter to search for.</param>
        /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
        /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
        public bool TryReadTo(out ReadOnlyMemory<T> memory, ReadOnlySpan<T> delimiter, bool advancePastDelimiter = true)
        {
            int currIndex = Consumed;

            if (!TryAdvanceTo(delimiter, false))
            {
                memory = default;
                return false;
            }

            memory = Memory[currIndex..Consumed];
            if (advancePastDelimiter)
                Advance(delimiter.Length);

            return true;
        }

        /// <summary>
        /// Attempts to read the exact amount of data as specified by <paramref name="count"/>.
        /// </summary>
        /// <param name="memory">The read data, if any.</param>
        /// <param name="count">The amount of data to read.</param>
        /// <returns><c>True</c> if the exact amount of data was able to be read, else <c>False</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="count"/> is negative.</exception>
        public bool TryReadExact(out ReadOnlyMemory<T> memory, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Count must not be a negative value");

            if (count > Remaining)
            {
                memory = default;
                return false;
            }

            memory = Memory.Slice(Consumed, count);
            Advance(count);

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

            int index = Memory.Span[Consumed..].IndexOf(delimiter);
            if (index == -1)
                return false;

            Advance(advancePastDelimiter ? index + 1 : index);
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

            int index = Memory.Span[Consumed..].IndexOf(delimiter);
            if (index == -1)
                return false;

            Advance(advancePastDelimiter ? index + delimiter.Length : index);
            return true;
        }
    }
}
