using System;

namespace bank_system
{
    class Account
    {
        private static FileHelper fileHelper = new FileHelper();
        User user;
        private int accountCounter;

        [Serializable]
        public class User
        {
            public string fName, lName, address, email;
            public int id, phoneNumber, balance;
        }

        public Account(int accountCounter)
        {
            this.accountCounter = accountCounter;
        }

        public Account()
        {
            
        }

        public void CreateAccount()
        {
            bool success = false;
            user = new User();
            GenerateId(user);
            user.balance = 0;

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
                Console.WriteLine("                      ║");

                Console.Write("║ Phone Number: ");
                int cursorPosLeftPhone = Console.CursorLeft;
                int cursorPosTopPhone = Console.CursorTop;
                Console.WriteLine("                 ║");

                Console.Write("║ Email: ");
                int cursorPosLeftEmail = Console.CursorLeft;
                int cursorPosTopEmail = Console.CursorTop;
                Console.WriteLine("                        ║");

                Console.WriteLine("╚═════════════════════════════════╝");

                Console.SetCursorPosition(cursorPosLeftFName, cursorPosTopFName);
                user.fName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftLName, cursorPosTopLName);
                user.lName = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftAddress, cursorPosTopAddress);
                user.address = Console.ReadLine();

                Console.SetCursorPosition(cursorPosLeftPhone, cursorPosTopPhone);
                string phoneInput = Console.ReadLine();
                phoneInput = phoneInput.Substring(0, 9);
                int.TryParse(phoneInput, out user.phoneNumber);

                Console.SetCursorPosition(cursorPosLeftEmail, cursorPosTopEmail);
                user.email = Console.ReadLine();

                
                Console.Write("\nIs the information correct (y/n)? ");
                int cursorPosLeftConfirm = Console.CursorLeft;
                int cursorPosTopConfirm = Console.CursorTop;

                Console.SetCursorPosition(cursorPosLeftConfirm, cursorPosTopConfirm);
                string confirm = Console.ReadLine();
                
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    success = true;
                    Console.WriteLine("Account created successfully! Details will be provided via email.");
                    Console.WriteLine("Account number is: {0}", user.id);
                    System.Threading.Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(500);
                }
            } while(!success);

            fileHelper.SerializeAccount(AccountFileName(user), user);
        }

        private User AccountLookup()
        {
            User user = new User();
            bool success = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Search for Account Number: ");
                int.TryParse(Console.ReadLine(), out int accountNumber);
                if (accountNumber > Constants.initialAccountCount && accountNumber <= accountCounter)
                {
                    Console.WriteLine("Account found! Loading account file...");
                    System.Threading.Thread.Sleep(500);
                    user = LoadAccount(accountNumber);
                    success = true;
                }
                else
                {
                    Console.WriteLine("Account does not exist. Please try again.");
                    System.Threading.Thread.Sleep(500);
                }
            }
            while (!success);

            return user;
        }

        public User SearchAccount()
        {
            bool success = false;

            do
            {
                Console.Clear();
                User user = AccountLookup();
                ViewAccount(user);
                Console.Write("Search for another account (y/n) ? ");
                string confirm = Console.ReadLine();
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    
                }
                else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    success = true;
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(500);
                }
            }
            while (!success);


            return user;
        }

        public void DeleteAccount()
        {
            bool success = false;

            do
            {
                User user = AccountLookup();
                Console.WriteLine("Delete Account (y/n)? ");
                string confirm = Console.ReadLine();
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    success = true;
                    Console.WriteLine("Account number {0} has been deleted", user.id);
                    System.Threading.Thread.Sleep(750);
                    fileHelper.DeleteAccountFile(user.id);
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(500);
                }
            }
            while (!success);
            
        }

        private void ViewAccount(User user)
        {
            Console.WriteLine("Account No: {0}", user.id);
            Console.WriteLine("Account Balance: ${0}", user.balance);
            Console.WriteLine("First Name: {0}", user.fName);
            Console.WriteLine("Last Name: {0}", user.fName);
            Console.WriteLine("Address: {0}", user.address);
            Console.WriteLine("Phone Number: {0}", user.phoneNumber);
            Console.WriteLine("Email: {0}", user.email);
        }

        public void Withdraw()
        {
            bool success = false;
            User user = AccountLookup();

            do
            {
                Console.Clear();

                Console.WriteLine("Account No: {0}", user.id);
                Console.Write("Amount: $");
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out int amount))
                {
                    Console.WriteLine("\n{0} Must be a number. Please input an amount.", userInput);
                    System.Threading.Thread.Sleep(500);
                }
                else if (amount > user.balance)
                {
                    Console.WriteLine("\nThe amount is greater than the balance. You may only withdraw less than or equal to the account balance.");
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    user.balance -= amount;
                    Console.WriteLine("Withdraw successful! The remaining balance for the user is: ${0}", user.balance);
                    fileHelper.SerializeAccount(AccountFileName(user), user);
                    success = true;
                    System.Threading.Thread.Sleep(750);
                }
            }
            while (!success);
        }

        public void Deposit()
        {
            bool success = false;
            User user = AccountLookup();

            do
            {
                Console.Clear();

                Console.WriteLine("Account No: {0}", user.id);
                Console.Write("Amount: $");
                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out int amount))
                {
                    Console.WriteLine("\n{0} Must be a number. Please input an amount.", userInput);
                    System.Threading.Thread.Sleep(500);
                }
                else if (amount <= -1)
                {
                    Console.WriteLine("\n Deposit can't be negative. Please input a positive number or use the withdraw functionality instead.");
                    System.Threading.Thread.Sleep(500);
                }
                else
                {
                    user.balance += amount;
                    Console.WriteLine("Deposit successful! The new balance for the user is: ${0}", user.balance);
                    fileHelper.SerializeAccount(AccountFileName(user), user);
                    success = true;
                    System.Threading.Thread.Sleep(750);
                }
            }
            while (!success);
        }

        public void AcStatement()
        {
            User user = AccountLookup();
            ViewAccount(user);
            Console.Write("Email statement (y/n) ? ");
            string confirm = Console.ReadLine();
            do
            {
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    Console.WriteLine("Statement sent to {0}. The email should arrive shortly.", user.id);
                    System.Threading.Thread.Sleep(750);
                    break;
                }
                else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(500);
                }
            }
            while (true);
        }

        private User LoadAccount(int accountNumber)
        {
            return fileHelper.DeserializeAccount(accountNumber);
        }

        private string AccountFileName(User user)
        {
            return  user.id + ".txt";
        }

        private void GenerateId(User user)
        {
            user.id = ++accountCounter;
        }
    } 
}
