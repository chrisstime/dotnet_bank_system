using System;

namespace bank_system
{
    class Menu
    {
        private static FileHelper fileHelper;
        private static Account accountManager;

        public Menu()
        {
            fileHelper = new FileHelper();
            accountManager = new Account(fileHelper.LoadAccounts());
        }

        public void MenuScreen()
        {
            int choice = -1;

            Console.Clear();
            do
            {
                Console.Clear();

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║   WELCOME TO ONLINE BANKING SYSTEM   ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine(
                    "║ 1. Create a new account.             ║\n" +
                    "║ 2. Search for an account.            ║\n" +
                    "║ 3. Deposit                           ║\n" +
                    "║ 4. Withdraw                          ║\n" +
                    "║ 5. A/C Statement                     ║\n" +
                    "║ 6. Delete Account                    ║\n" +
                    "║ 7. Exit                              ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.Write("║ Enter your choice (1-7): ");
                int cursorPosLeftChoice = Console.CursorLeft;
                int cursorPosTopChoice = Console.CursorTop;
                Console.WriteLine("            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.SetCursorPosition(cursorPosLeftChoice, cursorPosTopChoice);
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out choice))
                {
                    Console.WriteLine("\n{0} is not an a number. Please input a number between 1-7.", userInput);
                    System.Threading.Thread.Sleep(500);
                }
                else if (choice < 1 || choice > 7)
                {
                    Console.WriteLine("\nPlease input a number between 1-7.", userInput);
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    MenuChoice(choice);
                }

            }
            while (true);
        }

        public void MenuChoice(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    accountManager.CreateAccount();
                    break;
                case 2:
                    accountManager.SearchAccount();
                    break;
                case 3:
                    accountManager.Deposit();
                    break;
                case 4:
                    accountManager.Withdraw();
                    break;
                case 5:
                    accountManager.AcStatement();
                    break;
                case 6:
                    accountManager.DeleteAccount();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
