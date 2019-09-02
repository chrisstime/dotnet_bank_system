using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace bank_system
{
    class FileHelper
    {
        private static string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private static string accountsDir = Path.Combine(projectDir, "Accounts");

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
            string newAccountFilePath = Path.Combine(accountsDir, textFile);
            try
            {

                if (File.Exists(newAccountFilePath))
                {                   
                    File.Delete(newAccountFilePath);
                }

                BinaryFormatter formatter = new BinaryFormatter();
                //File.WriteAllLines(newAccountFilePath, content, Encoding.UTF8);
                Stream stream = new FileStream(newAccountFilePath, FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, user);
                stream.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
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
                throw;
            }
            finally
            {
                fs.Close();
            }

            return user;
        }
    }
}
