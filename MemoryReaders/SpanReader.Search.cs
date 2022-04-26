using System;
using System.Runtime.CompilerServices;

namespace MemoryReaders
{
    public ref partial struct SpanReader<T> where T : unmanaged, IEquatable<T>
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

            if (!Span[Consumed].Equals(value))
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

            if (!Span[Consumed..].StartsWith(value))
                return false;

            if (advancePast)
                Consumed += value.Length;

            return true;
        }

        /// <summary>
        /// Attempts to read everything up to the given <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="span">The read data, if any.</param>
        /// <param name="delimiter">The delimiter to search for.</param>
        /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
        /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
        public bool TryReadTo(out ReadOnlySpan<T> span, T delimiter, bool advancePastDelimiter = true)
        {
            int currIndex = Consumed;

            if (!TryAdvanceTo(delimiter, false))
            {
                span = default;
                return false;
            }

            span = Span[currIndex..Consumed];
            if (advancePastDelimiter)
                Consumed++;

            return true;
        }

        /// <summary>
        /// Attempts to read everything up to the given <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="span">The read data, if any.</param>
        /// <param name="delimiter">The delimiter to search for.</param>
        /// <param name="advancePastDelimiter"><c>True</c> to move past the <paramref name="delimiter"/>, if found.</param>
        /// <returns><c>True</c> if the given <paramref name="delimiter"/> was found, otherwise <c>False</c>.</returns>
        public bool TryReadTo(out ReadOnlySpan<T> span, ReadOnlySpan<T> delimiter, bool advancePastDelimiter = true)
        {
            int currIndex = Consumed;

            if (!TryAdvanceTo(delimiter, false))
            {
                span = default;
                return false;
            }

            span = Span[currIndex..Consumed];
            if (advancePastDelimiter)
                Advance(delimiter.Length);

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

            int index = Span[Consumed..].IndexOf(delimiter);
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

            int index = Span[Consumed..].IndexOf(delimiter);
            if (index == -1)
                return false;

            Advance(advancePastDelimiter ? index + delimiter.Length : index);
            return true;
        }
    }
}
