using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    class BankSystem
    {
        private static Login myBankLogin;
        private static Menu menu;

        /*
         * Main method for the entire program.
         */
        private static void Main()
        {
            myBankLogin = new Login();            
            menu = new Menu();
            Console.Clear();

            FileHelper.CreateDirectory(Constants.accountsDir);
            bool loginSuccess;

            // attempt login until credentials are correct.
            do
            {
                loginSuccess = myBankLogin.LoginInterface();
            }
            while (!loginSuccess);

            if(loginSuccess)
                menu.MenuScreen();
        }
    }
}
