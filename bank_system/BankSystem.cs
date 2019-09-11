﻿using System;
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

        public static void Main(string[] args)
        {
            myBankLogin = new Login();            
            menu = new Menu();
            Console.Clear();

            FileHelper.CreateDirectory(Constants.accountsDir);
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
