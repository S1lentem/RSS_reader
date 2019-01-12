using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSSController
{
    public static class StringSupport
    {
        public static bool IsOneOfNull<T> (this T[] elemnts)
            where T : class
        {
            foreach (var element in elemnts)
            {
                if (elemnts == null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string ClearFromHtml(this string str) => Regex.Replace(str, "<[^>]*(>|$)", string.Empty);
    } 

}
