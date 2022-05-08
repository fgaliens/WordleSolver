using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordleSolver.Models;
using WordleSolver.Sources;

namespace WordleSolver.Search
{
    public class WordleVocabulary
    {
        private readonly IVocabularyReader vocabularyReader;
        private readonly ISource source;
        
        private WordleWord[] words;

        public WordleVocabulary(IVocabularyReader vocabularyReader, ISource source)
        {
            this.vocabularyReader = vocabularyReader ?? throw new ArgumentNullException(nameof(vocabularyReader));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public bool Initialized { get; protected set; }
        public IEnumerable<WordleWord> Words => ReturnIfInitialized(words);
        public int Count => ReturnIfInitialized(words.Length);

        public async Task Initialize()
        {
            if (Initialized)
                return;

            var vocabularyModel = await vocabularyReader.ReadAsync();
            var sourceHash = await source.GetHashAsync();

            if (vocabularyModel.Hash != sourceHash)
            {
                var data = await source.GetDataAsync();

                vocabularyModel = new WordleVocabularyStoreModel
                {
                    Hash = sourceHash,
                    Words = data.ToArray()
                };

                await vocabularyReader.WriteAsync(vocabularyModel);
            }

            words = vocabularyModel.Words.Select(i => new WordleWord(i)).ToArray();

            Initialized = true;
        }

        protected void CheckInitialized()
        {
            if (!Initialized)
                throw new InvalidOperationException($"{nameof(WordleVocabulary)} hasn't been initialized");
        }

        protected T ReturnIfInitialized<T>(T value)
        {
            CheckInitialized();
            return value;
        }
    }
}
