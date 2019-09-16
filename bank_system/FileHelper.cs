/*
 * 31927 Application Development with .NET - Assignment 1
 * Author: Christine Vinaviles
 * Student No. 11986282
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace bank_system
{
    class FileHelper
    {
        /*
         * Method to create a directory within the project path.
         * Params: name for folder to be created.
         */
        public static void CreateDirectory(string subDirectory)
        {
            string newDir = Path.Combine(Constants.projectDir, subDirectory);
            if (!File.Exists(newDir))
                Directory.CreateDirectory(newDir);
        }

        /*
         * Method to read the contents of the file.
         * Params: the path of the file to read.
         * Returns a string array or each read line from the file.
         */
        public static string[] ReadFile(string textFile)
        {
            string[] fileContent = {};
            string textFilePath = Path.Combine(Constants.projectDir, textFile);
            try
            {
                if (File.Exists(textFilePath))
                    fileContent = File.ReadAllLines(textFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return fileContent;
        }

        /*
         * Method to load the current account count so existing accounts dont get overwritten on program restart.
         * Returns the current account count.
         */
        public static int LoadAccounts()
        {
            int accountCount;

            if (File.Exists(Constants.accountTracker))
            {
                int.TryParse(ReadFile(Constants.accountTracker)[0], out accountCount);
            }
            else
            {
                accountCount = Constants.initialAccountCount;
            }

            return accountCount;
        }

        /*
         * Method to store the user object into a text file.
         * Params: the user object to be stored.
         * Return a boolean to indicate whether storing the object to a text file was successful or not.
         */
        public static bool SerializeAccount(User user)
        {
            string newAccountFilePath = AccountPath(user.Id);
            try
            {

                if (!AccountExists(user.Id))
                {                   
                    File.Delete(newAccountFilePath);
                }

                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(newAccountFilePath, FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, user);
                stream.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        /*
         * Method to deserialize an object file.
         * Params: the account number of the user to deserialize.
         * Returns a user object.
         */
        public static User DeserializeAccount(int accountNumber)
        {
            User user = new User();
            FileStream fs = new FileStream(AccountPath(accountNumber), FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                user = (User)formatter.Deserialize(fs);
            }

            catch (SerializationException e)
            {
                throw e;
            }
            finally
            {
                fs.Close();
            }

            return user;
        }

        /*
         * Method to delete an account from the Accounts folder.
         * Params: the account number of the user to delete.
         * Returns a boolean to indicate whether account file deletion was successful or unsuccessful.
         */
        public static bool DeleteAccountFile(int accountNumber)
        {
            bool success = false;

            try
            {
                if (AccountExists(accountNumber))
                {
                    File.Delete(AccountPath(accountNumber));
                    success = true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }

        /*
         * Method that return the complete account path for specified account number.
         * Params: the account number
         * Returns a string complete path for the account file.
         */
        private static string AccountPath(int accountNumber)
        {
            return Path.Combine(Constants.accountsDir, accountNumber + ".txt");
        }

        /*
         * Wrapper method for checking if a an account exists.
         * Params: account number.
         * Returns a boolean to indicate whether the account exists or not.
         */
        public static bool AccountExists(int accountNumber)
        {
            return File.Exists(AccountPath(accountNumber));
        }

        /*
         * Method for saving the current account count.
         * Params: the last account count.
         */
        public static void SaveAccountCount(int accountCounter)
        {
            try
            {
                File.WriteAllText(Constants.accountTracker, accountCounter.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
