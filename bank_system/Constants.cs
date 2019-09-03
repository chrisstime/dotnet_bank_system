using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    class Constants
    {
        public static readonly string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static readonly string accountsDir = Path.Combine(projectDir, "Accounts");
    }
}
