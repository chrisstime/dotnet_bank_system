using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace bank_system
{
    class FileHelper
    {
        public static void CreateDirectory(string subDirectory)
        {
            string newDir = Path.Combine(Constants.projectDir, subDirectory);
            if (!File.Exists(newDir))
                Directory.CreateDirectory(newDir);
        }

        public static string[] ReadFile(string textFile)
        {
            string[] fileContent = File.ReadAllLines(textFile);

            return fileContent;
        }

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

        public static bool SerializeAccount(string textFile, User user)
        {
            bool success = false;
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

                success =  true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }

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

        private static string AccountPath(int accountNumber)
        {
            return Path.Combine(Constants.accountsDir, accountNumber + ".txt");
        }

        public static bool AccountExists(int accountNumber)
        {
            return File.Exists(AccountPath(accountNumber));
        }

        public static void SaveAccountCount(string accountCounter)
        {
            try
            {
                File.WriteAllText(Constants.accountTracker, accountCounter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
