using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main()
    {
        var inputFirstString = Console.ReadLine();
        var inputSecondString = Console.ReadLine();
        var dateDiff = DateModifier.DateDiff(inputFirstString, inputSecondString);

        Console.WriteLine(dateDiff);
    }
}

