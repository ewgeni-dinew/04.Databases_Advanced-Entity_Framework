using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        var inputLines = int.Parse(Console.ReadLine());
        var listOfPersons = new List<Person>();

        for (int i = 0; i < inputLines; i++)
        {
            var line = Console.ReadLine().Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            var name = line[0];
            var age = int.Parse(line[1]);

            var currentPerson = new Person(name,age);
            listOfPersons.Add(currentPerson);
        }

        foreach (var person in listOfPersons.Where(p=>p.Age>30).OrderBy(p=>p.Name))
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }
    }

}

