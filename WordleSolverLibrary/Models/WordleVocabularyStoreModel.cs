using System;

namespace WordleSolver.Models
{
    [Serializable]
    public class WordleVocabularyStoreModel
    {
        public string Hash { get; set; }
        public string[] Words { get; set; }
    }
}
