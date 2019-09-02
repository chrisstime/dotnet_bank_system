using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bank_system
{
    class Account
    {
        [Serializable]
        public class User
        {
            public string fName, lName, address, email;
            public int phoneNumber;

        }

        private bool success = false;

        public void AccountScreen()
        {
            User newUser = new User();
            do
            {
                Console.Clear();
               
                Console.WriteLine("╔═════════════════════════════════╗");
                Console.WriteLine("║     CREATE A NEW ACCOUNT        ║");
                Console.WriteLine("╠═════════════════════════════════╣");
                Console.WriteLine("║       ENTER THE DETAILS         ║");

                Console.Write("║ First Name: ");
                int cursorPosLeftFName = Console.CursorLeft;
                int cursorPosTopFName = Console.CursorTop;
                Console.WriteLine("                    ║");

                Console.Write("║ Last Name: ");
                int cursorPosLeftLName = Console.CursorLeft;
                int cursorPosTopLName = Console.CursorTop;
                Console.WriteLine("                     ║");

                Console.Write("║ Address: ");
                int cursorPosLeftAddress = Console.CursorLeft;
                int cursorPosTopAddress = Console.CursorTop;
                Console.WriteLine("                        ║");

                Console.Write("║ Phone Number: ");
                int cursorPosLeftPhone = Console.CursorLeft;
                int cursorPosTopPhone = Console.CursorTop;
                Console.WriteLine("                 ║");

                Console.Write("║ Email: ");
                int cursorPosLeftEmail = Console.CursorLeft;
                int cursorPosTopEmail = Console.CursorTop;
                Console.WriteLine("                     ║");

                Console.WriteLine("╚═════════════════════════════════╝");

                Console.SetCursorPosition(cursorPosLeftFName, cursorPosTopFName);
                newUser.fName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftLName, cursorPosTopLName);
                newUser.lName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftAddress, cursorPosTopAddress);
                newUser.address = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftPhone, cursorPosTopPhone);
                string phoneInput = Console.ReadLine();
                phoneInput = phoneInput.Substring(0, 10);
                int.TryParse(phoneInput, out newUser.phoneNumber);

                Console.SetCursorPosition(cursorPosLeftEmail, cursorPosTopEmail);
                newUser.email = Console.ReadLine();

                
                Console.Write("\nIs the information correct (y/n)? ");
                int cursorPosLeftConfirm = Console.CursorLeft;
                int cursorPosTopConfirm = Console.CursorTop;

                Console.SetCursorPosition(cursorPosLeftConfirm, cursorPosTopConfirm);
                string confirm = Console.ReadLine();
                
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    success = CreateAccount(newUser);
                }
            } while(!success);

        }

        private bool CreateAccount(User newUser)
        {
            //string[] user = { newUser.fName, newUser.lName, newUser.address, newUser.phoneNumber.ToString(), newUser.email };
            FileHelper fileHelper = new FileHelper();
            fileHelper.CreateAccount("12345.txt", newUser);

            fileHelper.OpenAccount("12345.txt");

            return true;
        }
    } 
}
