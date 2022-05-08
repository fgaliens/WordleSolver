using System;
using System.Linq;
using WordleSolver.Models;

namespace WordleSolver.Search.SearchTokens
{
    public class ContainedLetterToken : ISearchToken
    {
        public ContainedLetterToken(char letter, int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index can't be less than zero", nameof(index));
            }

            Letter = char.ToUpper(letter, System.Globalization.CultureInfo.CurrentCulture);
            Index = index;
        }

        public char Letter { get; }
        public int Index { get; }

        public bool IsMatch(WordleWord word)
        {
            if (Index >= word.Length)
                return false;

            if (word.Value[Index] == Letter)
                return false;

            return word.Value.Any(c => c == Letter);
        }
    }
}
