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
        private static FileHelper fileHelper;
        private static Menu menu;

        public static void Main(string[] args)
        {
            myBankLogin = new Login();            
            menu = new Menu();
            fileHelper = new FileHelper();
            Console.Clear();

            fileHelper.CreateDirectory(Constants.accountsDir);
            fileHelper.LoadAccounts();
            bool loginSuccess = false;

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
