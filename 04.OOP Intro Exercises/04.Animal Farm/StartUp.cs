using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main()
    {
        var name = Console.ReadLine();
        var age = int.Parse(Console.ReadLine());

        try
        {
            var chicken = new Chicken(name, age);

            Console.WriteLine(
                "Chicken {0} (age {1}) can produce {2} eggs per day.",
                chicken.Name,
                chicken.Age,
                chicken.GetProductPerDay());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

