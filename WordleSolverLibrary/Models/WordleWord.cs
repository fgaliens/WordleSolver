using System;
using System.Text.RegularExpressions;

namespace WordleSolver.Models
{
    public class WordleWord
    {
        public WordleWord(string word)
        {
            if (!Regex.IsMatch(word, @"\w+"))
                throw new ArgumentException($"Input word '{word}' contains unallowed char(s)", nameof(word));

            Value = word.ToUpper();
        }

        public string Value { get; }
        public int Length => Value.Length;

        public override string ToString() => Value;
        

        public char this[int index] { get => Value[index]; }
    }
}
