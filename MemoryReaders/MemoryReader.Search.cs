using System;
using System.Runtime.CompilerServices;

namespace MemoryReaders
{
    public partial struct MemoryReader<T> where T : unmanaged, IEquatable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNext(T value, bool advancePast = false)
        {
            if (!TryPeek(out T next))
                return false;

            if (!next.Equals(value))
                return false;

            if (advancePast)
                Consumed++;

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

            int index = Memory.Span[Consumed..].IndexOf(delimiter);
            if (index == -1)
                return false;

            Consumed = advancePastDelimiter ? index + delimiter.Length : index;
            return true;
        }
    }
}
