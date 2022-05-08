using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WordleSolver.Sources
{
    public class FileVocabularySource : ISource
    {
        private readonly IFormatter formatter;
        private readonly IFilter filter;

        public FileVocabularySource(FileLoadingOptions loadingOptions, IFormatter formatter, IFilter filter)
        {
            Path = loadingOptions.VocabularyPath;
            Encoding = loadingOptions.Encoding;
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            this.filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public string Path { get; }
        public Encoding Encoding { get; }

        public async Task<string> GetHashAsync()
        {
            return await Task.Run(() => 
            { 
                using var sha = SHA256.Create();
                using var fs = File.OpenRead(Path);
                var hash = sha.ComputeHash(fs);

                return Convert.ToBase64String(hash);
            });
        }

        public async Task<IEnumerable<string>> GetDataAsync()
        {
            static bool TryReadLine(StreamReader streamReader, IFormatter formatter, out string value)
            {
                string line = streamReader.ReadLine();
                if (line != null)
                {
                    value = formatter.Format(line);
                    return true;
                }

                value = null;
                return false;
            }

            return await Task.Run(() => 
            {
                using StreamReader reader = File.OpenText(Path);

                List<string> words = new();

                while (TryReadLine(reader, formatter, out string value))
                {
                    if (filter.IsValid(value))
                    {
                        words.Add(value);
                    }
                }

                return (IEnumerable<string>)words;
            });
        }
    }
}
