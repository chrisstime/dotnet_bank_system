/*
 * 31927 Application Development with .NET - Assignment 1
 * Author: Christine Vinaviles
 * Student No. 11986282
 */
using System;
using System.IO;

namespace bank_system
{
    /*
     * Formbox sections represented as an enum.
     */
    public enum FormBox
    {
        header, footer, formDivider
    };

    /*
     * Text styles represented as an enum.
     */
    public enum FontStyle
    {
        h1, h2, currency, label
    };

    /*
     * Constants values used for the program.
     */
    class Constants
    {
        public static readonly string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static readonly string accountsDir = Path.Combine(projectDir, "Accounts");
        public static readonly string accountTracker = Path.Combine(accountsDir, "accountCount");
        public static int initialAccountCount = 100000;
        public static readonly int defaultFormLength = 40;
        
    }
}
