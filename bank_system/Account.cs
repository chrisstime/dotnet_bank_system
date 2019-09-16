using System;
using System.IO;

namespace bank_system
{
    class Account
    {
        /*
         * Method to create an account.
         */
        public void CreateAccount()
        {
            User user = new User();

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

                // set cursor position and read user inputs for the forms.
                user.FName = FormHelper.ReadFormField(cursorPosFName);
                user.LName = FormHelper.ReadFormField(cursorPosLName);
                user.Address = FormHelper.ReadFormField(cursorPosAddress);
                user.PhoneNumber = FormHelper.ReadFormFieldNumber(cursorPosPhone);
                user.Email = FormHelper.ReadFormFieldEmail(cursorPosEmail);

                Console.Write("\nIs the information correct (y/n)? ");
                int cursorPosLeftConfirm = Console.CursorLeft;
                int cursorPosTopConfirm = Console.CursorTop;

                Console.SetCursorPosition(cursorPosLeftConfirm, cursorPosTopConfirm);
                string confirm = Console.ReadLine();
                
                if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    FileHelper.SerializeAccount(user);
                    FileHelper.SaveAccountCount(user.Id);
                    Console.WriteLine("\nAccount created successfully! Details will be provided via email.");
                    Console.WriteLine("Account number is: {0}", user.Id);
                    System.Threading.Thread.Sleep(1500);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(1000);
                }
            } while(true);
        }

        /*
         * Method for looking up an account within the accounts directory.
         * Param: the title for the screen which describes the context on which the account is being searched, 
         *        i.e. searching if an account exists for deletion, deposit or withdrawal.
         */
        private User AccountLookup(string heading)
        {
            _ = new User();
            User user;

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
                    user = FileHelper.DeserializeAccount(accountNumber);
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

        /*
         * Method for the search interface in the program.
         */
        public User Search()
        {
            _ = new User();
            User user;

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
                    System.Threading.Thread.Sleep(1000);

                }
            }
            while (true);

            return user;
        }

        /*
         * Method to delete an account.
         */
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
                    success = FileHelper.DeleteAccountFile(user.Id);
                    if (success)
                        Console.WriteLine("Account number {0} has been deleted", user.Id);
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                    System.Threading.Thread.Sleep(1000);
                }
                System.Threading.Thread.Sleep(1000);
            }
            while (!success);
            
        }

        /*
         * Method to print the account details of an account.
         * Param: user object.
         */
        private void ViewAccount(User user)
        {
            FormHelper.DrawFormBox(FormBox.header);
            FormHelper.Heading("ACCOUNT DETAILS", FontStyle.h1);
            FormHelper.Body("Account No: " + user.Id);
            FormHelper.Body("Account Balance: $" + user.Balance);
            FormHelper.Body("First Name: " + user.FName);
            FormHelper.Body("Last Name: " + user.LName);
            FormHelper.Body("Address: " + user.Address);
            FormHelper.Body("Phone Number: " + user.PhoneNumber);
            FormHelper.Body("Email: " + user.Email);
            FormHelper.DrawFormBox(FormBox.footer);
        }

        /*
         * Method to withdraw money from an account, provided that they have sufficient balance.
         */
        public void Withdraw()
        {
            bool success = false;
            User user = AccountLookup("WITHDRAW");

            do
            {
                Console.Clear();

                FormHelper.Heading("WITHDRAW", FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                FormHelper.Body("Account No: " + user.Id);
                int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                FormHelper.DrawFormBox(FormBox.footer);

                string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);
                int amount = int.Parse(inputAmount);
                
                // Only allow withdrawal if the amount being withdrawn is greater than or equal to the account balance.
                if (amount > user.Balance)
                {
                    Console.WriteLine("\n\nThe amount is greater than the balance. You may only withdraw less than or equal to the account balance.");
                }
                else
                {
                    user.Balance -= amount;
                    Console.WriteLine("\n\nWithdraw successful! The remaining balance for the user is: ${0}", user.Balance);
                    success = FileHelper.SerializeAccount(AccountFileName(user.Id), user);
                }
                System.Threading.Thread.Sleep(1000);
            }
            while (!success);
        }

        /*
         * Method for depositing values into an account.
         */
        public void Deposit()
        {
            User user = AccountLookup("DEPOSIT");

            do
            {
                Console.Clear();
                FormHelper.Heading("DEPOSIT", FontStyle.h1);
                FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                FormHelper.Body("Account No: " + user.Id);
                int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                FormHelper.DrawFormBox(FormBox.footer);

                string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);
                int amount = int.Parse(inputAmount);

                user.Balance += amount;
                Console.WriteLine("\n\nDeposit successful! The new balance for the user is: ${0}", user.Balance);
                FileHelper.SerializeAccount(user);
                System.Threading.Thread.Sleep(1000);
                break;
            }
            while (true);
        }

        /*
         * Screen for searching and displaying the account statement for an existing account.
         */
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
                    Console.WriteLine("Statement sent to {0}. The email should arrive shortly.", user.Id);
                    break;
                }
                else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n' only.");
                }
            }
            while (true);

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);

        }
    } 
}
