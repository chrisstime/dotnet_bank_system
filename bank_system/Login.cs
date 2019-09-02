using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    class Login
    {
        string userName, passWord;

        public bool LoginInterface()
        {
            do
            {
                Console.Clear();

                Console.WriteLine("╔═════════════════════════════════╗");
                Console.WriteLine(" WELCOME TO ONLINE BANKING SYSTEM");
                Console.WriteLine("╠═════════════════════════════════╣");
                Console.WriteLine("\tLogin to Start\n");

                Console.Write("Username: ");
                int cursorPosLeftUserName = Console.CursorLeft;
                int cursorPosTopUserName = Console.CursorTop;

                Console.Write("\nPassword: ");
                int cursorPosLeftPwd = Console.CursorLeft;
                int cursorPosTopPwd = Console.CursorTop;

                /* \n is essential so it doesn't affect the input interface*/
                Console.WriteLine("\n=================================");
                Console.SetCursorPosition(cursorPosLeftUserName, cursorPosTopUserName);
                userName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftPwd, cursorPosTopPwd);

                string passwdChar = "*";

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
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
            while(true);

            Console.WriteLine("Valid Credentials! Logging in...");
            System.Threading.Thread.Sleep(1000);

            return Authenticate(userName, passWord);
        }

        private bool Authenticate(string userName, string passWord)
        {
            FileHelper fileHelper = new FileHelper();
            string[] fileContent = fileHelper.ReadFile("login.txt");
            bool allowAccess = false;
            char delimeterChar = ' ';

            foreach (string line in fileContent)
            {
                string[] credentials = line.Split(delimeterChar);
                allowAccess = String.Equals(credentials[0], userName) && String.Equals(credentials[1], passWord);
            }

            return allowAccess;
        }
    }
}
