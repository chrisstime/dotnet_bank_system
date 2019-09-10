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
        public bool Confirm(string YMessage, string NMessage)
        {
            string confirm = Console.ReadLine();

            if (String.Equals(confirm.ToLower(), 'y'.ToString()))
            {
                Console.WriteLine(YMessage);
                System.Threading.Thread.Sleep(750);
                return true;
            }
            else if (String.Equals(confirm.ToLower(), 'n'.ToString()))
            {
                Console.WriteLine(NMessage);
                System.Threading.Thread.Sleep(750);
            }
            else
            {
                Console.WriteLine("Please enter 'y' or 'n' only.");
                System.Threading.Thread.Sleep(750);
            }
            return false;
        }
    }
}
