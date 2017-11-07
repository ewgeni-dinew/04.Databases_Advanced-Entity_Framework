using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DateModifier
{
    public static int DateDiff(string first,string second)
    {
        var dateOne = DateTime.ParseExact(first, "yyyy MM dd", null);
        var dateTwo = DateTime.ParseExact(second, "yyyy MM dd", null);
        var difference = Math.Abs((int)(dateOne - dateTwo).TotalDays);

        return difference;
    }
}

