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
        public static bool Confirm(string yMessage, string nMessage)
        {
            string confirm = Console.ReadLine();

            if (String.Equals(confirm.ToLower(), 'y'.ToString()))
            {
                Console.WriteLine(yMessage);
                System.Threading.Thread.Sleep(1000);
                return true;
            }
            else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
            {
                Console.WriteLine(nMessage);
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n' only.");
                System.Threading.Thread.Sleep(1000);
            }
            return false;
        }

        public static int[] FormField(string formLabel, FontStyle fontStyle)
        {
            string formattedLabel = formLabel + (fontStyle == FontStyle.currency ? ": $" : ": ");
            Console.Write("║ ");
            int[] cursorPos = { Console.CursorLeft + formattedLabel.Length, Console.CursorTop };
            Console.Write(formattedLabel.PadRight(Constants.defaultFormLength));
            Console.WriteLine("║");

            return cursorPos;
        }

        public static string ReadFormField(int[] cursorPos)
        {
            Console.SetCursorPosition(cursorPos[0], cursorPos[1]);

            return Console.ReadLine();
        }

        public static string ReadFormFieldNumber(int[] cursorPos)
        {
            Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    if (Char.IsNumber(key.KeyChar) && input.Length < 10)
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

        public static string ReadFormFieldEmail(int[] cursorPos)
        {
            string input;
            string pattern = "(.)+(@gmail.com | @uts.edu.au | @outlook.com)";
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            do
            {
                Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
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
                    System.Threading.Thread.Sleep(1500);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    ClearCurrentConsoleLine();
                }
                    
            }
            while (true);

            return input;
        }

        public static void Heading(string heading, FontStyle headingStyle)
        {
            int padding = Constants.defaultFormLength - heading.Length;
            int preTitlePadding;

            if (padding % 2 == 1)
            {
                preTitlePadding = (padding - 1) / 2;
            }
            else
            {
                preTitlePadding = padding / 2;
            }

            Console.Write("║ ".PadRight(preTitlePadding));
            Console.Write(heading.PadRight(Constants.defaultFormLength - preTitlePadding));
            Console.WriteLine("  ║");

            if (headingStyle == FontStyle.h1)
            {
                DrawFormBox(FormBox.formDivider);
            }
            else if (headingStyle == FontStyle.h2)
            {
                Console.Write("║ ");
                Console.Write("".PadRight(Constants.defaultFormLength));
                Console.WriteLine("║");
            }
        }

        public static void Body(string text)
        {
            Console.Write("║ ");
            Console.Write(text.PadRight(Constants.defaultFormLength));
            Console.WriteLine("║");
        }

        private static void ClearCurrentConsoleLine()
        {
            System.Threading.Thread.Sleep(1000);
            int cursorPos = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorPos);
        }

        public static void DrawFormBox(FormBox formBox)
        {
            switch (formBox)
            {
                case FormBox.header:
                    Console.WriteLine("╔═════════════════════════════════════════╗");
                    break;
                case FormBox.footer:
                    Console.WriteLine("╚═════════════════════════════════════════╝");
                    break;
                case FormBox.formDivider:
                    Console.WriteLine("╠═════════════════════════════════════════╣");
                    break;
            }
        }
    }
}
