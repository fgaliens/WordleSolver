using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WordleSolver;
using WordleSolver.Search;
using WordleSolver.Sources;

namespace SolverConsole.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureWordleFileSource<TFormatter, TFilter>(this IServiceCollection services, string sourceVocabularyPath)
            where TFormatter : class, IFormatter where TFilter : class, IFilter
        {
            FileLoadingOptions loadingOptions = new()
            {
                VocabularyPath = sourceVocabularyPath
            };

            services
                .AddSingleton(loadingOptions)
                .AddTransient<IFormatter, TFormatter>()
                .AddTransient<IFilter, TFilter>()
                .AddTransient<ISource, FileVocabularySource>();

            return services;
        }

        public static IServiceCollection ConfigureWordleServices(this IServiceCollection services)
        {
            var sourceSrvice = services.FirstOrDefault(s => s.ServiceType == typeof(ISource));
            if (sourceSrvice == default)
                throw new InvalidOperationException("Unable to configure Wordle services because source was not defined");
            
            WordleVocabularyOptions vocabularyOptions = new()
            {
                VocabularyPath = "wordle.vcb"
            };

            services
                .AddSingleton(vocabularyOptions)
                .AddTransient<IVocabularyReader, WordleVocabularyReader>()
                .AddSingleton<WordleVocabulary>()
                .AddSingleton<WordsPopularity>()
                .AddSingleton<ISearcher, WordsPopularity>();

            return services;
        }
    }
}
