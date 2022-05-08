using System.Text;

namespace WordleSolver.Sources
{
    public class FileLoadingOptions
    {
        public string VocabularyPath { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }
}
