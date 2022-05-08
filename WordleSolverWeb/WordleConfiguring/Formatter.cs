using System.Text.RegularExpressions;
using WordleSolver.Sources;

namespace WordleSolverWeb.WordleConfiguring
{
    public class Formatter : IFormatter
    {
        private readonly Regex formatter = new(@"(?<word>^\w+)", RegexOptions.Compiled);

        public string Format(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var value = input
                    .Replace("&#769;", "")
                    .Replace("ё", "е")
                    .Replace("Ё", "Е");

                var match = formatter.Match(value);
                if (match.Success)
                {
                    return match.Groups["word"].Value.ToLower(System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            return string.Empty;
        }
    }
}
