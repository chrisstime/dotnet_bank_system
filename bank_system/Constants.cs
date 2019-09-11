using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    public enum FormBox
    {
        header, footer, formDivider
    };

    public enum FontStyle
    {
        h1, h2, currency, label
    };

    class Constants
    {
        public static readonly string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static readonly string accountsDir = Path.Combine(projectDir, "Accounts");
        public static readonly string accountTracker = Path.Combine(accountsDir, "accountCount");
        public static int initialAccountCount = 100000;
        public static readonly int defaultFormLength = 40;
        
    }
}
