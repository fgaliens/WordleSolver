using System;
using WordleSolver.Models;

namespace WordleSolver.Search
{
    public class WordPopularity
    {
        public WordPopularity(WordleWord word, /*int variety,*/ int popularity)
        {
            if (popularity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(popularity));
            }

            Word = word ?? throw new ArgumentNullException(nameof(word));
            Popularity = popularity;
        }

        public WordleWord Word { get; }
        public int Popularity { get; }
    }
}
