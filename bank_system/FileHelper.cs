using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace bank_system
{
    class FileHelper
    {
        private static readonly string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private static readonly string accountsDir = Path.Combine(projectDir, "Accounts");

        public void CreateDirectory(string subDirectory)
        {
            Directory.CreateDirectory(Path.Combine(projectDir, subDirectory));
        }

        public string[] ReadFile(string textFile)
        {
            string[] fileContent = File.ReadAllLines(textFile);

            return fileContent;
        }

        public bool CreateAccount(string textFile, Account.User user)
        {
            bool success = false;
            string newAccountFilePath = Path.Combine(accountsDir, textFile);
            try
            {

                if (File.Exists(newAccountFilePath))
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

        public Account.User OpenAccount(string accountNumber)
        {
            Account.User user = new Account.User();
            string filePath = Path.Combine(accountsDir, accountNumber);
            FileStream fs = new FileStream(filePath, FileMode.Open);
            
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                user = (Account.User)formatter.Deserialize(fs);
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

        public bool DeleteAccount(string accountNumber)
        {
            string filePath = Path.Combine(accountsDir, accountNumber);
            bool success = false;

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(Path.Combine(filePath));
                    success = true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }
    }
}
