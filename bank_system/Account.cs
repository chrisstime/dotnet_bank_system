using System;
using System.IO;

namespace bank_system
{
    class Account
    {
        private int accountCounter;

        [Serializable]
        public class User
        {
            public int id, balance;
            public string fName, lName, address, phoneNumber, email;
        }

        public Account()
        {
            this.accountCounter = FileHelper.LoadAccounts();
        }

        public void CreateAccount()
        {
            bool success = false;
            User user = new User();
            GenerateId(user);
            user.balance = 0;

            do
            {
                Console.Clear();

                FormHelper.DrawFormBox(FormBox.header);
                FormHelper.Heading("CREATE A NEW ACCOUNT", FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);

                int[] cursorPosFName = FormHelper.FormField("First Name", FontStyle.label);
                int[] cursorPosLName = FormHelper.FormField("Last Name", FontStyle.label);
                int[] cursorPosAddress = FormHelper.FormField("Address", FontStyle.label);
                int[] cursorPosPhone = FormHelper.FormField("Phone Number", FontStyle.label);
                int[] cursorPosEmail = FormHelper.FormField("Email", FontStyle.label);

                FormHelper.DrawFormBox(FormBox.footer);

                user.fName = FormHelper.ReadFormField(cursorPosFName);
                user.lName = FormHelper.ReadFormField(cursorPosLName);
                user.address = FormHelper.ReadFormField(cursorPosAddress);
                user.phoneNumber = FormHelper.ReadFormFieldNumber(cursorPosPhone);
                user.email = FormHelper.ReadFormFieldEmail(cursorPosEmail);

                
                Console.Write("\nIs the information correct (y/n)? ");
                int cursorPosLeftConfirm = Console.CursorLeft;
                int cursorPosTopConfirm = Console.CursorTop;

                Console.SetCursorPosition(cursorPosLeftConfirm, cursorPosTopConfirm);
                string confirm = Console.ReadLine();
                
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    success = true;
                    Console.WriteLine("\n\nAccount created successfully! Details will be provided via email.");
                    Console.WriteLine("Account number is: {0}", user.id);
                    System.Threading.Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(500);
                }
            } while(!success);

            FileHelper.SerializeAccount(AccountFileName(user), user);
            FileHelper.SaveAccountCount(accountCounter.ToString());
        }

        private User AccountLookup(string heading)
        {
            User user = new User();

            do
            {
                Console.Clear();
                FormHelper.DrawFormBox(FormBox.header);
                FormHelper.Heading(heading, FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                int[] cursorPosInput = FormHelper.FormField("Search for Account Number", FontStyle.label);
                FormHelper.DrawFormBox(FormBox.footer);
                int.TryParse(FormHelper.ReadFormField(cursorPosInput), out int accountNumber);

                if (FileHelper.AccountExists(accountNumber))
                {
                    Console.WriteLine("\nAccount found! Loading account file...");
                    System.Threading.Thread.Sleep(500);
                    user = LoadAccount(accountNumber);
                    break;
                }
                else
                {
                    Console.WriteLine("\nAccount does not exist. Please try again.");
                    System.Threading.Thread.Sleep(1000);
                }     
            }
            while (true);

            return user;
        }

        public User Search()
        {
            User user = new User();
            bool success = false;

            do
            {
                Console.Clear();
                user = AccountLookup("SEARCH AN ACCOUNT");
                ViewAccount(user);

                Console.Write("Search for another account (y/n) ? ");
                string confirm = Console.ReadLine();

                if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    break;
                }
                else if (!String.Equals(confirm.ToLower(), 'y'.ToString()))
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
                User user = AccountLookup("DELETE AN ACCOUNT");
                Console.WriteLine("Delete Account (y/n)? ");
                string confirm = Console.ReadLine();
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    
                    Console.WriteLine("Account number {0} has been deleted", user.id);
                    success = FileHelper.DeleteAccountFile(user.id);
                    System.Threading.Thread.Sleep(750);
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(750);
                }
            }
            while (!success);
            
        }

        private void ViewAccount(User user)
        {
            FormHelper.DrawFormBox(FormBox.header);
            FormHelper.Heading("ACCOUNT DETAILS", FontStyle.h1);
            FormHelper.Body("Account No: " + user.id);
            FormHelper.Body("Account Balance: $" + user.balance);
            FormHelper.Body("First Name: " + user.fName);
            FormHelper.Body("Last Name: " + user.fName);
            FormHelper.Body("Address: " + user.address);
            FormHelper.Body("Phone Number: " + user.phoneNumber);
            FormHelper.Body("Email: " + user.email);
            FormHelper.DrawFormBox(FormBox.footer);
        }

        public void Withdraw()
        {
            bool success = false;
            User user = AccountLookup("WITHDRAW");

            do
            {
                Console.Clear();

                FormHelper.Heading("WITHDRAW", FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                FormHelper.Body("Account No: " + user.id);
                int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                FormHelper.DrawFormBox(FormBox.footer);

                string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);

                if (!int.TryParse(inputAmount, out int amount))
                {
                    Console.WriteLine("\n{0} Must be a number. Please input an amount.", inputAmount);
                    System.Threading.Thread.Sleep(1000);
                }
                else if (amount > user.balance)
                {
                    Console.WriteLine("\nThe amount is greater than the balance. You may only withdraw less than or equal to the account balance.");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    user.balance -= amount;
                    Console.WriteLine("\nWithdraw successful! The remaining balance for the user is: ${0}", user.balance);
                    FileHelper.SerializeAccount(AccountFileName(user), user);
                    success = true;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            while (!success);
        }

        public void Deposit()
        {
            User user = AccountLookup("DEPOSIT");

            do
            {
                Console.Clear();
                FormHelper.Heading("DEPOSIT", FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                FormHelper.Body("Account No: " + user.id);
                int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                FormHelper.DrawFormBox(FormBox.footer);

                string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);

                if (!int.TryParse(inputAmount, out int amount))
                {
                    Console.WriteLine("\n\n Amount must be a number. Please input an amount.", inputAmount);
                    System.Threading.Thread.Sleep(1000);
                }
                else if (amount <= -1)
                {
                    Console.WriteLine("\n\n Deposit can't be negative. Please input a positive number or use the withdraw functionality instead.");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    user.balance += amount;
                    Console.WriteLine("\n\n Deposit successful! The new balance for the user is: ${0}", user.balance);
                    FileHelper.SerializeAccount(AccountFileName(user), user);
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
            }
            while (true);
        }

        public void AcStatement()
        {
            User user = AccountLookup("STATEMENT");
            ViewAccount(user);
            Console.Write("Email statement (y/n) ? ");
            string confirm = Console.ReadLine();
            do
            {
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    Console.WriteLine("Statement sent to {0}. The email should arrive shortly.", user.id);
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
                else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(750);
                }
            }
            while (true);
        }

        private User LoadAccount(int accountNumber)
        {
            return FileHelper.DeserializeAccount(accountNumber);
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
