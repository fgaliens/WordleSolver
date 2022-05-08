using WordleSolver.Models;

namespace WordleSolver.Search
{
    public interface ISearchToken
    {
        bool IsMatch(WordleWord word);
    }
}
