/*
 * 31927 Application Development with .NET - Assignment 1
 * Author: Christine Vinaviles
 * Student No. 11986282
 */
using System;

namespace bank_system
{
    class Login
    {
        string userName, passWord;

        /*
         * Method for the login interface.
         * Returns a boolean to indicate successful user login.
         */
        public bool LoginInterface()
        {
            do
            {
                Console.Clear();

                FormHelper.DrawFormBox(FormBox.header);
                FormHelper.Heading("WELCOME TO ONLINE BANKING SYSTEM", FontStyle.h1);
                FormHelper.Heading("Login to Start", FontStyle.h2);

                int[] cursorPosUserName = FormHelper.FormField("User Name", FontStyle.label);
                int[] cursorPosPassword = FormHelper.FormField("Password", FontStyle.label);

                FormHelper.DrawFormBox(FormBox.footer);

                userName = FormHelper.ReadFormField(cursorPosUserName);

                Console.SetCursorPosition(cursorPosPassword[0], cursorPosPassword[1]);

                string passwdChar = "*";

                // keep converting the characters into * until user presses enter.
                do
                {
                    ConsoleKeyInfo Key = Console.ReadKey(true);
                    if (Key.Key != ConsoleKey.Backspace && Key.Key != ConsoleKey.Enter)
                    {
                        passWord += Key.KeyChar;
                        Console.Write(passwdChar);
                    }
                    else if (Key.Key == ConsoleKey.Backspace && Key.Key != ConsoleKey.Enter && passWord.Length > 0)
                    {
                        passWord = passWord.Substring(0, (passWord.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (Key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                } while (true);

                Console.WriteLine("\n\nAuthenticating...");
                if (!Authenticate(userName, passWord))
                {
                    passWord = "";
                    userName = "";
                    Console.WriteLine("Invalid Credentials. Press Try Again.");
                    System.Threading.Thread.Sleep(750);
                }
                else
                {
                    break;
                }
            }
            while(true);

            Console.WriteLine("Valid Credentials! Logging in...");
            System.Threading.Thread.Sleep(500);

            return true;
        }

        /*
         * Method to authenticate username and password input against the login text file.
         * Params: the username and password entered by the user.
         * Returns a boolean to indicate whether or not the entered username and password matched any pairs from the file.
         */
        private bool Authenticate(string userName, string passWord)
        {
            string[] fileContent = FileHelper.ReadFile("login.txt");
            bool allowAccess = false;
            char delimeterChar = '|';

            foreach (string line in fileContent)
            {
                string[] credentials = line.Split(delimeterChar);
                allowAccess = String.Equals(credentials[0], userName) && String.Equals(credentials[1], passWord);
                if (allowAccess)
                    return allowAccess;
            }

            return allowAccess;
        }
    }
}
