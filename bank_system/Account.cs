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
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int[] PhoneNumber { get; set; }

        private bool success = false;

        public void AccountScreen()
        {
            do
            {
                Console.Clear();
               
                Console.WriteLine("╔═════════════════════════════════╗");
                Console.WriteLine("║     CREATE A NEW ACCOUNT        ║");
                Console.WriteLine("╠═════════════════════════════════╣");
                Console.WriteLine("║       ENTER THE DETAILS         ║");

                Console.Write("║ First name: ");
                int cursorPosLeftFName = Console.CursorLeft;
                int cursorPosTopFName = Console.CursorTop;
                Console.WriteLine("                    ║");

                Console.Write("║ Last name: ");
                int cursorPosLeftLName = Console.CursorLeft;
                int cursorPosTopLName = Console.CursorTop;
                Console.WriteLine("                     ║");
                Console.WriteLine("╚═════════════════════════════════╝");

                Console.SetCursorPosition(cursorPosLeftFName, cursorPosTopFName);
                string fName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftLName, cursorPosTopLName);
                string lName = Console.ReadLine();

                success = true;
            } while(!success);
        }
        
    } 
}
