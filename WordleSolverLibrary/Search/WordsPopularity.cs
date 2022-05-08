using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordleSolver.Models;

namespace WordleSolver.Search
{

    public class WordsPopularity : ISearcher
    {
        private readonly WordleVocabulary vocabulary;
        private List<WordPopularity> popularityList;

        public WordsPopularity(WordleVocabulary vocabulary)
        {
            this.vocabulary = vocabulary ?? throw new ArgumentNullException(nameof(vocabulary));
        }

        public bool Initialized { get; private set; }

        public async Task Initialize()
        {
            if (Initialized)
                return;

            await vocabulary.Initialize();

            LettersPopularity popularity = new(vocabulary);
            popularityList = new(vocabulary.Count);

            HashSet<string> lookedUpWords = new();

            foreach (var word in vocabulary.Words)
            {
                var value = word.Value;
                if (!lookedUpWords.Contains(value))
                {
                    HashSet<char> lookedUpLetters = new();
                    var score = 0;

                    foreach (var letter in value)
                    {
                        if (!lookedUpLetters.Contains(letter))
                        {
                            score += popularity[letter];
                            lookedUpLetters.Add(letter);
                        }
                    }

                    popularityList.Add(new(word, score));
                    
                    lookedUpWords.Add(value);
                }
            }

            popularityList.Sort((w1, w2) => w2.Popularity - w1.Popularity);

            Initialized = true;
        }

        public IEnumerable<WordleWord> Search(IEnumerable<ISearchToken> tokens)
        {
            CheckInitialized();
            return new SearchResultEnumerator(popularityList.Select(i => i.Word), tokens);
        }

        protected void CheckInitialized()
        {
            if (!Initialized)
                throw new InvalidOperationException($"{nameof(WordleVocabulary)} hasn't been initialized");
        }
    }

    public class SearchResultEnumerator : IEnumerable<WordleWord>
    {
        private readonly IEnumerable<WordleWord> words;
        private readonly IEnumerable<ISearchToken> tokens;

        public SearchResultEnumerator(IEnumerable<WordleWord> words, IEnumerable<ISearchToken> tokens)
        {
            this.words = words ?? throw new ArgumentNullException(nameof(words));
            this.tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            
            TokensCount = tokens.Count();
        }

        public int TokensCount { get; }

        public IEnumerator<WordleWord> GetEnumerator()
        {
            foreach (var word in words)
            {
                if (tokens.All(t => t.IsMatch(word)) || TokensCount == 0)
                {
                    yield return word;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
