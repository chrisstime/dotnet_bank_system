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
            MyBankLogin.LoginScreen();

            Console.Read();
        }
    }
}
