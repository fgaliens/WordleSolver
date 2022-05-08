using System.Threading.Tasks;
using WordleSolver.Models;

namespace WordleSolver.Sources
{
    public interface IVocabularyReader
    {
        Task<WordleVocabularyStoreModel> ReadAsync();
        Task WriteAsync(WordleVocabularyStoreModel vocabulary);
    }
}