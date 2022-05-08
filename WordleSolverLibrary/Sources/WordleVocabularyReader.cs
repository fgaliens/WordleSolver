using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WordleSolver.Models;

namespace WordleSolver.Sources
{
    public class WordleVocabularyReader : IVocabularyReader
    {
        public WordleVocabularyReader(WordleVocabularyOptions options)
        {
            Path = options.VocabularyPath;
        }

        public string Path { get; }

        public async Task<WordleVocabularyStoreModel> ReadAsync()
        {
            if (File.Exists(Path))
            {
                var json = await File.ReadAllTextAsync(Path);
                return JsonSerializer.Deserialize<WordleVocabularyStoreModel>(json);
            }

            return new();
        }

        public async Task WriteAsync(WordleVocabularyStoreModel vocabulary)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(vocabulary, options);
            await File.WriteAllTextAsync(Path, json);
        }
    }
}
