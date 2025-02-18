
using System;

namespace BF_SubclassList
{
    public static class Helpers
    {
        public static string GetMiddleString(this string @string, string start, string end)
        {
            int pFrom = @string.IndexOf(start, StringComparison.Ordinal) + start.Length;
            int pTo = @string.LastIndexOf(end, StringComparison.Ordinal);
            return @string.Substring(pFrom, pTo - pFrom);
        }
    }
}