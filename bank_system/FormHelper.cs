using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    /*
     * Helper class to help with input validations.
     */
    class FormHelper
    {
        public bool Confirm(string SuccessMessage)
        {
            string confirm = Console.ReadLine();

            if (String.Equals(confirm.ToLower(), 'y'.ToString()))
            {
                Console.WriteLine(SuccessMessage);
                System.Threading.Thread.Sleep(1500);
                return true;
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n' only.");
                System.Threading.Thread.Sleep(500);
                return false;
            }
        }
    }
}
