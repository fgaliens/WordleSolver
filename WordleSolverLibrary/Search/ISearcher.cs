using System.Collections.Generic;
using System.Threading.Tasks;
using WordleSolver.Models;
using WordleSolver.Search.SearchTokens;

namespace WordleSolver.Search
{

    public interface ISearcher
    {
        Task Initialize();
        IEnumerable<WordleWord> Search(IEnumerable<ISearchToken> tokens);
    }
}
