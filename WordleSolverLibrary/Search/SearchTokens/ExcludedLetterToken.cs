using System.Linq;
using WordleSolver.Models;

namespace WordleSolver.Search.SearchTokens
{
    public class ExcludedLetterToken : ISearchToken
    {
        public ExcludedLetterToken(char letter)
        {
            Letter = char.ToUpper(letter, System.Globalization.CultureInfo.CurrentCulture);
        }

        public char Letter { get; }

        public bool IsMatch(WordleWord word)
        {
            return !word.Value.Any(c => c == Letter);
        }
    }
}
