using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Car
{
    public string Model { get; set; }
    public double FuelAmount { get; set; }
    public double ConsumptionPerKm { get; set; }
    public double Distance { get; set; }

    public Car(string model,double fuelAm,double consumption)
    {
        this.Model = model;
        this.FuelAmount = fuelAm;
        this.ConsumptionPerKm = consumption;
        this.Distance = 0;
    }

    public bool Move(double distance)
    {
        double fuelNeeded = distance * this.ConsumptionPerKm;
        if (this.FuelAmount<fuelNeeded)
        {
            return false;
        }
        else
        {
            this.FuelAmount -= fuelNeeded;
            this.Distance += distance;
            return true;
        }
    }
}

