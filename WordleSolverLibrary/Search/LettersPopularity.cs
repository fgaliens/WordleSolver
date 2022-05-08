using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WordleSolver.Search
{
    public class LettersPopularity
    {
        private readonly Dictionary<char, int> popularity = new();

        public LettersPopularity(WordleVocabulary vocabulary)
        {
            foreach (var word in vocabulary.Words)
            {
                foreach (var letter in word.Value)
                {
                    if (!popularity.ContainsKey(letter))
                    {
                        popularity.Add(letter, 1);
                    }
                    else
                    {
                        popularity[letter]++;
                    }
                }
            }
        }

        public IReadOnlyDictionary<char, int> Popularity => popularity;
        public int this[char letter] => popularity[letter];

        public bool Contains(char letter) => popularity.ContainsKey(letter);
    }
}
