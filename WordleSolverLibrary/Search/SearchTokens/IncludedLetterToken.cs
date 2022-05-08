using System;
using WordleSolver.Models;

namespace WordleSolver.Search.SearchTokens
{
    public class IncludedLetterToken : ISearchToken
    {
        public IncludedLetterToken(char letter, int index)
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

            return word.Value[Index] == Letter;
        }
    }
}
