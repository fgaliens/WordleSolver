using Microsoft.AspNetCore.Mvc;
using WordleSolver.Search;
using WordleSolver.Search.SearchTokens;
using WordleSolverWeb.Models;

namespace WordleSolverWeb.Controllers
{
    [Route("api")]
    [ApiController]
    public class WordleSearchController : Controller
    {
        private readonly ISearcher searcher;

        public WordleSearchController(ISearcher searcher)
        {
            this.searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
        }

        [HttpPost("search-guesses")]
        public async Task<IActionResult> SearchGuesses(SearchGuessesRequest request)
        {
            await searcher.Initialize();

            if (request is { Tokens: not null, Limit: > 0, Offset: >= 0 })
            {    
                var includedTokens = request.Tokens
                    .Where(t => t.State == SearchTokenState.Included)
                    .Select(t => new IncludedLetterToken(t.Letter, t.Index))
                    .Cast<ISearchToken>();

                var containedTokens = request.Tokens
                    .Where(t => t.State == SearchTokenState.Contains)
                    .Select(t => new ContainedLetterToken(t.Letter, t.Index))
                    .Cast<ISearchToken>();

                var excludedTokens = request.Tokens
                    .Where(t => t.State == SearchTokenState.Excluded)
                    .Select(t => new ExcludedLetterToken(t.Letter))
                    .Cast<ISearchToken>();

                var tokens = includedTokens
                    .Union(containedTokens)
                    .Union(excludedTokens);

                var result = searcher
                    .Search(tokens)
                    .Skip(request.Offset)
                    .Take(request.Limit);

                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
