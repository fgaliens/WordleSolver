using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WordleSolver.Search;

namespace WordleSolver.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static async Task<ISearcher> GetWordleSearcherAsync(this IServiceProvider provider)
        {
            var wordlePopularity = provider.GetRequiredService<WordsPopularity>();
            await wordlePopularity.Initialize();
            
            return wordlePopularity;
        }

        public static ISearcher GetWordleSearcher(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<WordsPopularity>();
            service.Initialize().Wait();

            return service;
        }
    }
}
