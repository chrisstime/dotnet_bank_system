using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    /*
     * User object used for storing the user account details.
     */
    [Serializable]
    class User
    {
        public int Id { get; } = FileHelper.LoadAccounts() + 1;
        public int Balance { get; set; } = 0;
        public string FName { get; set; } = "";
        public string LName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
