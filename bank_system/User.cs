/*
 * 31927 Application Development with .NET - Assignment 1
 * Author: Christine Vinaviles
 * Student No. 11986282
 */
using System;

namespace bank_system
{
    /*
     * User object used for storing the user account details.
     */
    [Serializable]
    class User
    {
        public int Id { get; } = FileHelper.LoadAccounts() + 1;
        public double Balance { get; set; } = 0.0;
        public string FName { get; set; } = "";
        public string LName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
