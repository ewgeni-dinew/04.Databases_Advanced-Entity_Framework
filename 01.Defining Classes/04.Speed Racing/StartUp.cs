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
        var listOfCars = new List<Car>();

        for (int i = 0; i < inputLines; i++)
        {
            var line = Console.ReadLine()
                .Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries);

            var carModel = line[0];
            var fuelAmount = double.Parse(line[1]);
            var consumption = double.Parse(line[2]);

            if (!listOfCars.Any(c=>c.Model==carModel))
            {
                var currentCar = new Car(carModel, fuelAmount, consumption);
                listOfCars.Add(currentCar);
            }
        }

        var command = Console.ReadLine();
        while (command!="End")
        {
            var inputCommand=command
               .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var checkCarModel = inputCommand[1];
            var checkDistance = double.Parse(inputCommand[2]);

            var car = listOfCars.Find(c => c.Model == checkCarModel);
            var isMoved = car.Move(checkDistance);

            if (!isMoved)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }

            command = Console.ReadLine();
        }

        foreach (var car in listOfCars)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.Distance}");
        }
    }
}

