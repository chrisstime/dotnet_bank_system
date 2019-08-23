using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    class Menu
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Login MyBankLogin = new Login();
            Menu MyMenu = new Menu();

            if (MyBankLogin.LoginScreen())
                MyMenu.MenuScreen();

            Console.Read();
        }

        public void MenuScreen()
        {
            Console.Clear();

            Console.WriteLine("WELCOME TO ONLINE BANKING SYSTEM");
            Console.WriteLine("=================================");
            Console.WriteLine(
                "1. Create a new account.\n" +
                "2. Search for an account.\n" +
                "3. Deposit\n" +
                "4. Withdraw\n" +
                "5. A/C Statement\n" +
                "6. Delete Account\n" +
                "7. Exit");
            Console.WriteLine("=================================");
            Console.Write("Enter your choice (1-7): ");
            int cursorPosLeftChoice = Console.CursorLeft;
            int cursorPosTopChoice = Console.CursorTop;
            Console.WriteLine("\n=================================");

            Console.SetCursorPosition(cursorPosLeftChoice, cursorPosTopChoice);
            string userInput = Console.ReadLine();
            if (String.Equals(userInput, '7'))
                Environment.Exit(0);

            Console.ReadKey();
        }

        public void MenuChoice(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
