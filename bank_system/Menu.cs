using System;

namespace bank_system
{
    class Menu
    {
        private static Account accountManager;

        public Menu()
        {
            accountManager = new Account();
        }

        /*
         * Method to print options for the menu screen.
         */
        public void MenuScreen()
        {
            Console.Clear();
            do
            {
                Console.Clear();

                FormHelper.DrawFormBox(FormBox.header);
                FormHelper.Heading("WELCOME TO ONLINE BANKING SYSTEM", FontStyle.h1);
                FormHelper.Body("1. Create a new account.");
                FormHelper.Body("2. Search for an account.");
                FormHelper.Body("3. Deposit.");
                FormHelper.Body("4. Withdraw.");
                FormHelper.Body("5. A/C Statement.");
                FormHelper.Body("6. Delete Account.");
                FormHelper.Body("7. Exit.");

                FormHelper.DrawFormBox(FormBox.formDivider);
                int[] cursorPosChoice = FormHelper.FormField("Enter your choice (1-7)", FontStyle.label);
 
                FormHelper.DrawFormBox(FormBox.footer);

                string userInput = FormHelper.ReadFormFieldNumber(cursorPosChoice);
                int choice = int.Parse(userInput);
                
                // Validate that input is an integer from 1-7.
                if (choice < 1 || choice > 7)
                {
                    Console.WriteLine("\n\nPlease input a number between 1-7.", choice);
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    MenuChoice(choice);
                }

            }
            while (true);
        }
        
        /*
         * Method call the appropriate function selected from the menu.
         * Params: integer representing the user choice.
         */
        private void MenuChoice(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    accountManager.CreateAccount();
                    break;
                case 2:
                    accountManager.Search();
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
                default:
                    break;
            }
        }
    }
}
