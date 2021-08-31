using System;

namespace NBS.Models
{
    public class HomeViewModel
    {
        public static string GetRandomStr(int iLen)
        {
            int i, iVal;
            Random rnd = new Random();
            string str = string.Empty;
            char[] ch = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for (i = 0; i < iLen; i++)
            {
                iVal = rnd.Next(1, ch.Length);
                if (!str.Contains(ch.GetValue(iVal).ToString()))
                    str += ch.GetValue(iVal);
                else i--;
            }
            return str;
        }
    }
}