using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Util
{
    class StringUtil
    {
        public static string ConvertToDisplayName(string name)
        {            
            string[] fs = name.Split('_');
            StringBuilder sb = new StringBuilder();

            foreach (string str in fs)
            {
                if (string.IsNullOrWhiteSpace(str)) continue;

                string s1 = str.Substring(0, 1);
                sb.Append(s1.ToUpper());

                if (str.Length > 1)
                    sb.Append(str.Substring(1));
            }
            return sb.ToString();
        }
    }
}
