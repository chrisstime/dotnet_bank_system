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

                bool confirm = Confirm("\nIs the information correct (y/n)? ");
                
                if (confirm)
                {
                    FileHelper.SerializeAccount(user);
                    FileHelper.SaveAccountCount(user.Id);
                    Console.WriteLine("\nAccount created successfully! Details will be provided via email.");
                    Console.WriteLine("Account number is: {0}", user.Id);
                    break;
                }
            } while(true);

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
        }

        /*
         * Method for looking up an account within the accounts directory.
         * Param: the title for the screen which describes the context on which the account is being searched, 
         *        i.e. searching if an account exists for deletion, deposit or withdrawal.
         * Returns the accountNumber if it exists or -1 if it doesn't.
         */
        private int SearchAccountNumberTo(string heading)
        {
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
                    return accountNumber;
                }
                else
                {
                    Console.WriteLine("\nAccount does not exist.");
                    bool searchAgain = Confirm("Search for another account (y/n) ? ");
                    if (!searchAgain)
                        return -1;
                }
            }
            while (true);
        }

        /*
         * Method for the search interface in the program.
         */
        public void Search()
        {
            _ = new User();
            User user;

            do
            {
                Console.Clear();
                int accountNumber = SearchAccountNumberTo("SEARCH AN ACCOUNT");
                if(accountNumber != -1)
                {
                    user = FileHelper.DeserializeAccount(accountNumber);
                    ViewAccount(user);
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                }
                else
                {
                    break;
                }
            }
            while (true);

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
        }

        /*
         * Method to delete an account.
         */
        public void DeleteAccount()
        {
            bool success = false;

            do
            {
                int accountNumber = SearchAccountNumberTo("DELETE AN ACCOUNT");
                if (accountNumber != -1)
                {
                    User user = FileHelper.DeserializeAccount(accountNumber);
                    bool confirm = Confirm("Delete Account (y/n)? ");

                    if (confirm)
                    {
                        success = FileHelper.DeleteAccountFile(user.Id);
                        if (success)
                            Console.WriteLine("Account number {0} has been deleted", user.Id);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            while (!success);

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
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
            do
            {
                int accountNumber = SearchAccountNumberTo("WITHDRAW");
                if (accountNumber != -1)
                {
                    User user = FileHelper.DeserializeAccount(accountNumber);
                    bool success = false;

                    do
                    {
                        Console.Clear();

                        FormHelper.Heading("WITHDRAW", FontStyle.h1);
                        FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                        FormHelper.Body("Account No: " + user.Id);
                        int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                        FormHelper.DrawFormBox(FormBox.footer);

                        string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);

                        // Only allow withdrawal if the amount being withdrawn is greater than or equal to the account balance.
                        if (double.TryParse(inputAmount, out double amount))
                        {
                            if (amount > user.Balance)
                            {
                                Console.WriteLine("\n\nThe amount is greater than the balance. You may only withdraw less than or equal to the account balance.");
                            }
                            else
                            {
                                user.Balance -= amount;
                                Console.WriteLine("\n\nWithdraw successful! The remaining balance for the user is: ${0}", user.Balance);
                                success = FileHelper.SerializeAccount(user);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Deposit unsuccessful. Please try again.");
                        }
                    }
                    while (!success);
                }
                else
                {
                    break;
                }
            } while (true);
            
            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
        }

        /*
         * Method for depositing values into an account.
         */
        public void Deposit()
        {
            do
            {
                int accountNumber = SearchAccountNumberTo("DEPOSIT");
                if (accountNumber != -1)
                {
                    User user = FileHelper.DeserializeAccount(accountNumber);

                    do
                    {
                        Console.Clear();
                        FormHelper.Heading("DEPOSIT", FontStyle.h1);
                        FormHelper.Heading("ENTER THE DETAILS", FontStyle.h2);
                        FormHelper.Body("Account No: " + user.Id);
                        int[] cursorPosAmount = FormHelper.FormField("Amount", FontStyle.currency);
                        FormHelper.DrawFormBox(FormBox.footer);

                        string inputAmount = FormHelper.ReadFormFieldNumber(cursorPosAmount);
                        if (double.TryParse(inputAmount, out double amount))
                        {
                            user.Balance += amount;

                            if (FileHelper.SerializeAccount(user))
                            {
                                Console.WriteLine("\n\nDeposit successful! The new balance for the user is: ${0}", user.Balance);
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Deposit unsuccessful. Please try again.");
                        }
                    }
                    while (true);
                }
                else
                {
                    break;
                }
            } while (true);
            

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
        }

        /*
         * Screen for searching and displaying the account statement for an existing account.
         */
        public void AcStatement()
        {
            do
            {
                int accountNumber = SearchAccountNumberTo("SEARCH AN ACCOUNT");
                if (accountNumber != -1)
                {
                    User user = FileHelper.DeserializeAccount(accountNumber);
                    ViewAccount(user);
                    bool emailStatement = Confirm("Email statement (y/n) ? ");
                    if (emailStatement)
                    {
                        Console.WriteLine("Statement sent to {0}. The email should arrive shortly.", user.Id);
                    }
                }
                else
                {
                    break;
                }
            } while (true);

            Console.WriteLine("Returning to main menu...");
            System.Threading.Thread.Sleep(1000);
        }

        private bool Confirm(string message)
        {
            do
            {
                Console.Write(message);
                string confirm = Console.ReadLine();
                FormHelper.ClearCurrentConsoleLine();
                if (String.Equals(confirm.ToLower(), 'n'.ToString()))
                {
                    return false;
                }
                else if (String.Equals(confirm.ToLower(), 'y'.ToString()))
                {
                    return true;
                }
                else
                {
                    Console.Write("Please enter 'y' or 'n' only.");
                    FormHelper.ClearCurrentConsoleLine();
                }
            }
            while (true);
        }
    } 
}
