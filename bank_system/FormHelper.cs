using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace bank_system
{
    /*
     * Helper class to help with input validations.
     */
    class FormHelper
    {
        public static bool Confirm(string YMessage, string NMessage)
        {
            string confirm = Console.ReadLine();

            if (String.Equals(confirm.ToLower(), 'y'.ToString()))
            {
                Console.WriteLine(YMessage);
                System.Threading.Thread.Sleep(750);
                return true;
            }
            else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
            {
                Console.WriteLine(NMessage);
                System.Threading.Thread.Sleep(750);
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n' only.");
                System.Threading.Thread.Sleep(750);
            }
            return false;
        }

        public static string ValidateMobileNumber()
        {
            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    if (Char.IsNumber(key.KeyChar) && input.Length < 9)
                    {
                        input += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return input;
        }

        public static string ValidateEmail(int cursorPosLeft, int cursorPosTop)
        {
            string input;
            string pattern = "(.)+(@gmail.com | @uts.edu.au | @outlook.com)";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            do
            {
                Console.SetCursorPosition(cursorPosLeft, cursorPosTop);
                input = Console.ReadLine();

                if (rg.IsMatch(input))
                {
                    break;
                }
                else
                {
                    Console.Write(
                        "\nEmail must have an @ and must belong to gmail.com, " +
                        "outlook.com or uts.edu.au domain."
                        );
                    Console.SetCursorPosition(0, Console.CursorTop);
                    ClearCurrentConsoleLine();
                }
                    
            }
            while (true);

            return input;
        }

        private static void ClearCurrentConsoleLine()
        {
            System.Threading.Thread.Sleep(1000);
            int cursorPos = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorPos);
        }
    }
}
