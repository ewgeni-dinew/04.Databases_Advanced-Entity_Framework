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
        var listOfCars = new List<Car>();

        for (int i = 0; i < inputRowCount; i++)
        {
            var line = Console.ReadLine()
                .Split();


            //2
            //ChevroletAstro 200 180 1000 fragile 1.3 1 1.5 2 1.4 2 1.7 4
//Citroen2CV 190 165 1200 fragile 0.9 3 0.85 2 0.95 2 1.1 1
//fragile

            var model = line[0];
            var engineSpeed = int.Parse(line[1]);
            var enginePower = int.Parse(line[2]);
            var cargoWeight = int.Parse(line[3]);
            var cargoType = line[4];

            var currentEngine = new Engine(engineSpeed,enginePower);
            var currentCargo = new Cargo(cargoWeight,cargoType);

            var listOfTires = new List<Tire>();
            for (int j = 5; j < 12; j+=2)
            {
                var pressureT = double.Parse(line[j]);
                var ageT = int.Parse(line[j+=1]);
                var currentTire = new Tire(pressureT,ageT);
                listOfTires.Add(currentTire);
                j -= 1;
            }

            var car = new Car(model, currentEngine, currentCargo, listOfTires);
            listOfCars.Add(car);
        }

        var command = Console.ReadLine();
        if (command=="fragile")
        {
            foreach (var car in listOfCars.Where(c=>c.Cargo.Type=="fragile"&&c.Tires.Any(p=>p.Pressure<1)))
            {
                Console.WriteLine(car.Model);
            }
        }
        else if (command=="flammable")
        {
            foreach (var car in listOfCars.Where(c => c.Cargo.Type == "flammable" && c.Engine.Power>250))
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}

