using Microsoft.Extensions.DependencyInjection;
using SolverConsole.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordleSolver.Extensions;
using WordleSolver.Search;
using WordleSolver.Search.SearchTokens;

namespace SolverConsole
{
    class Program
    {
        private const int SearchResultLimit = 10;

        public static void Main(string[] args)
        {
            Console.Title = "Wordle Helper";
            WConsole.SystMsg("Welcome to Wordle Helper!");
            WConsole.SystMsg("Loading...");

            var searcher = GetSearcher();

            WConsole.ClearLine(-1);
            WConsole.SystMsg("If you want to find the word, just type all what you know about this word in such format:");
            WConsole.SystMsg("'A- C+ B? P- E+'");
            WConsole.SystMsg("That mean if you shure that the first letter is not 'A' type 'A-'");
            WConsole.SystMsg("If you shure that the second letter is 'C' type 'C+'");
            WConsole.SystMsg("And if you know that the third letter is 'B', but the word contains this letter type 'B?'");
            WConsole.SystMsg();
            WConsole.SystMsg("Also:");
            WConsole.SystMsg("Type 'New' to find new word");
            WConsole.SystMsg("Type 'Exit' for exit");
            WConsole.SystMsg("Press Enter to start! Let's go!");
            WConsole.SystMsg();

            WConsole.Msg("Some words to start with:");
            bool continueLoop = true;

            while (continueLoop)
            {
                bool continueGuessingWord = true;

                var tokens = new List<ISearchToken>();

                while (continueGuessingWord)
                {
                    var result = searcher.Search(tokens).Take(SearchResultLimit);

                    if (!result.Any())
                    {
                        WConsole.Msg("<nothing>");
                    }

                    foreach (var word in result)
                    {
                        WConsole.Msg(word.ToString());
                    }
                    WConsole.Msg();

                    var command = WConsole.UserInput();

                    var textTokens = Regex
                        .Matches(command, @"(\w[+\-?])")
                        .Select((match, index) => (Chr: match.Value[0], Symbol: match.Value[1], Index: index));

                    if (textTokens.Any())
                    {
                        foreach (var textToken in textTokens)
                        {
                            if (textToken.Symbol == '+')
                            {
                                tokens.Add(new IncludedLetterToken(textToken.Chr, textToken.Index));
                                WConsole.WriteIncluded(textToken.Chr);
                            }
                            else if (textToken.Symbol == '-')
                            {
                                tokens.Add(new ExcludedLetterToken(textToken.Chr));
                                WConsole.WriteExcluded(textToken.Chr);
                            }
                            else if (textToken.Symbol == '?')
                            {
                                tokens.Add(new ContainedLetterToken(textToken.Chr, textToken.Index));
                                WConsole.WriteContained(textToken.Chr);
                            }
                        }

                        WConsole.Msg();
                    }
                    else if (Regex.IsMatch(command, @"new", RegexOptions.IgnoreCase))
                    {
                        continueGuessingWord = false;
                    }
                    else if (Regex.IsMatch(command, @"exit", RegexOptions.IgnoreCase))
                    {
                        continueGuessingWord = false;
                        continueLoop = false;
                    }
                    else
                    {
                        WConsole.SystMsg($"Unknown command '{command}'");
                    }
                }
            }

            Console.BackgroundColor = ConsoleColor.White;
        }

        private static ISearcher GetSearcher()
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureWordleFileSource<Formatter, Filter>("LopatinVocab.txt")
                .ConfigureWordleServices()
                .BuildServiceProvider();

            return serviceProvider.GetWordleSearcher();
        }
    }
}
