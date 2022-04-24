using System;

namespace MemoryReaders
{
    public ref partial struct SpanReader<T> where T : unmanaged, IEquatable<T>
    {
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

            Consumed = advancePastDelimiter ? index + 1 : index;
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

            Consumed = advancePastDelimiter ? index + delimiter.Length : index;
            return true;
        }
    }
}
