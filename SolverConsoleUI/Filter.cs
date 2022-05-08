﻿using System.Text.RegularExpressions;
using WordleSolver.Sources;

namespace SolverConsole
{
    public class Filter : IFilter
    {
        private readonly Regex regex = new(@"^[\w]{5}$", RegexOptions.Compiled);

        public bool IsValid(string input) =>
            regex.IsMatch(input);
    }
}
