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
        public bool Confirm(string YMessage, string NMessage)
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

        public string MobileNumberValidator()
        {
            //int mobileNumber;
            //do
            //{

            //    if (Regex.Match(Number, @"^(\[0-9]{10})$").Success)
            //    {
            //        int.TryParse(Number, out mobileNumber);
            //        break;
            //    }
            //} while (true);

            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    //int val = 0;
                    //bool keyIsANumber = int.TryParse(key.KeyChar.ToString(), out val);
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
    }
}
