using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main()
    {
        var inputRowCount = int.Parse(Console.ReadLine());
        var family = new Family();

        for (int i = 0; i < inputRowCount; i++)
        {
            var line = Console
                .ReadLine()
                .Split();

            var currentName = line[0];
            var currentAge = int.Parse(line[1]);

            var currentPerson = new Person(currentName, currentAge);
            family.AddMember(currentPerson);
        }

        Console.WriteLine(family.GetOldestMember(family));
    }
}

