using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bank_system
{
    class Login
    {
        static string userName, passWord;

        public void LoginScreen()
        {
            Console.Clear();

            Console.WriteLine("WELCOME TO ONLINE BANKING SYSTEM");
            Console.WriteLine("=================================");
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
            if (Authenticate(userName, passWord))
            {
                Console.WriteLine("Valid Credentials! Press Enter to Continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Invalid Credentials. Press Enter to Try Again.");
                Console.ReadKey();
                LoginScreen();
            }
        }

        private bool Authenticate(string userName, string passWord)
        {
            string fileContent;
            bool allowAccess = false;
            StreamReader file = new StreamReader(@"login.txt");
            while ((fileContent = file.ReadLine()) != null)
            {
                char delimeterChar = ' ';
                string[] credentials = fileContent.Split(delimeterChar);

                allowAccess = (credentials[0] == userName && credentials[1] == passWord);
            }
            file.Close();

            return allowAccess;
        }
    }
}
