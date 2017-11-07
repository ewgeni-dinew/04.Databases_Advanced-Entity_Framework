using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tire
{
    private double pressure;
    private int age;

    public Tire(double pressure, int age)
    {
        this.Pressure = pressure;
        this.age = age;
    }

    public double Pressure
    {
        get { return this.pressure; }
        set { this.pressure = value; }
    }

    public int Age
    {
        get { return this.age; }
        set { this.age = value; }
    }
}

