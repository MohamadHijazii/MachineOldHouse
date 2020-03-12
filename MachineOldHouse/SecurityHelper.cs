using System;
using System.Collections.Generic;
using System.Text;

namespace MachineOldHouse
{
    public class SecurityHelper
    {
        public static string getHash(string s)
        {
            StringBuilder hash = new StringBuilder();
            char[] t = s.ToCharArray();
            int n = 65;
            int i = 0;
            for(i=0;i<t.Length;i++)
            {
                if (i % 4 !=0 ) continue;
                n = n + t[i];
                n = n % 128;
                hash.Append(n);
                if (i == 40) break;
            }
            return hash.ToString();
        }
    }
}
