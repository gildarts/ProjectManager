using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Util
{
    class CalcUtil
    {
        public static string GetVisualSize(decimal size)
        {
            int i = 0;
            decimal result = 0;
            for (i = 1; i <= 4; i++)
            {
                decimal unit1 = (decimal)Math.Pow(1024, i - 1);
                decimal unit2 = (decimal)Math.Pow(1024, i);
                if (size > unit1 && size <= unit2)
                {
                    result = Math.Round(size / unit1, 2);
                    break;
                }
            }

            if (result == 0)
                return size + " Bytes";
            if (i == 1)
                return result + " Bytes";
            if (i == 2)
                return result + " KB";
            if (i == 3)
                return result + " MB";
            if (i == 4)
                return result + " GB";
            return size + " Bytes";
        }
    }
}
