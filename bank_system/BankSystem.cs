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
        private static Constants bankConstant;
        private static Account accountManager;
        private static Menu menu;

        public static void Main(string[] args)
        {
            myBankLogin = new Login();
            bankConstant = new Constants();
            fileHelper = new FileHelper();
            accountManager = new Account(100000);
            menu = new Menu();
            Console.Clear();

            fileHelper.CreateDirectory("Accounts");
            bool loginSuccess = false;

            do
            {
                loginSuccess = myBankLogin.LoginInterface();
            }
            while (!loginSuccess);

            if(loginSuccess)
                menu.MenuScreen();

            Console.Read();
        }
    }
}
