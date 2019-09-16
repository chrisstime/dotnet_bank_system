/*
 * 31927 Application Development with .NET - Assignment 1
 * Author: Christine Vinaviles
 * Student No. 11986282
 */
using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace bank_system
{
    /*
     * Helper class to help with input validations for the program.
     */
    class FormHelper
    {
        /*
         * Formfield method automatically formats an input field.
         * Params: label of the field input as a string, an font style enum.
         * Returns the cursor position of the beginning of the input field as an array of integer.
         * cursorPos[0] represents the left cursor position and cursorPos[1] represents the top cursor position.
         */
        public static int[] FormField(string formLabel, FontStyle fontStyle)
        {
            // Add a dollar sign if the input field is for money.
            string formattedLabel = formLabel + (fontStyle == FontStyle.currency ? ": $" : ": ");
            Console.Write("║ ");
            int[] cursorPos = { Console.CursorLeft + formattedLabel.Length, Console.CursorTop };

            // Pad the text right to make the box even.
            Console.Write(formattedLabel.PadRight(Constants.defaultFormLength));
            Console.WriteLine("║");

            return cursorPos;
        }

        /*
         * Read formfield method to read the line at a given cursor position.
         * Params: integer array representing the cursor position to read from.
         * Returns the read user input string.
         */
        public static string ReadFormField(int[] cursorPos)
        {
            Console.SetCursorPosition(cursorPos[0], cursorPos[1]);

            return Console.ReadLine();
        }

        /*
         * Method to read a number input from a formfield. Prevents user from typing anything that isn't a number.
         * Params: integer array representing the cursor position to read from.
         * Returns the resulting user input string.
         */
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

        /*
         * Method to read and validate an email input.
         * Params: integer array representing the cursor position to read from.
         * Returns validate email string from user input.
         */
        public static string ReadFormFieldEmail(int[] cursorPos)
        {
            string input;
            // regex pattern for the email string.
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
                    System.Threading.Thread.Sleep(600);
                    ClearCurrentConsoleLine();
                }
                    
            }
            while (true);

            return input;
        }

        /*
         * Method for formatting heading for the bank program prints the result to consult.
         * Params: string label for the heading, enum to specify the fontstyle.
         */
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

        /*
         * Method to format body text within the program form. 
         * Params: text to print in body.
         */
        public static void Body(string text)
        {
            Console.Write("║ ");
            Console.Write(text.PadRight(Constants.defaultFormLength));
            Console.WriteLine("║");
        }

        /*
         * Method to clear a single line at the current cursor position.
         */
        public static void ClearCurrentConsoleLine()
        {
            System.Threading.Thread.Sleep(1000);
            int cursorPos = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorPos);
        }

        /*
         * Method to draw the form outline for the program.
         * Params: enum to specify what kind of outline to print.
         */
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
