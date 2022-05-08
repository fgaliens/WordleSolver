using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverConsole
{
    public static class WConsole
    {
        public static void SystMsg(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
        }

        public static void Msg(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
        }

        public static void WriteIncluded(char value)
        {
            WriteChar(value, ConsoleColor.Black, ConsoleColor.Green);
        }

        public static void WriteContained(char value)
        {
            WriteChar(value, ConsoleColor.Black, ConsoleColor.Yellow);
        }

        public static void WriteExcluded(char value)
        {
            WriteChar(value, ConsoleColor.Black, ConsoleColor.Gray);
        }

        public static string UserInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return Console.ReadLine();
        }

        public static void ClearLine(int offset = 0, bool returnToLine = false)
        {
            Console.CursorTop += offset;
            Console.CursorLeft = 0;

            var s = string.Join(' ', Enumerable.Range(0, Console.WindowWidth)
                .Select(s => string.Empty)
                .ToArray());
            Console.Write(s);
            
            Console.CursorLeft = 0;

            if (returnToLine)
            {
                Console.CursorTop -= offset;
            }
        }

        private static void WriteChar(char value, ConsoleColor frgrnd, ConsoleColor bcgrnd)
        {
            var frgClr = Console.ForegroundColor;
            var bcgClr = Console.BackgroundColor;

            Console.ForegroundColor = frgrnd;
            Console.BackgroundColor = bcgrnd;

            Console.Write(value);

            Console.ForegroundColor = frgClr;
            Console.BackgroundColor = bcgClr;
        }
    }
}
