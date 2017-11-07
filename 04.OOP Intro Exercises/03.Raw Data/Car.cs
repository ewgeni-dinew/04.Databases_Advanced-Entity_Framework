using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Car
{
    private string model;
    private Engine engine;
    private Cargo cargo;
    private List<Tire> tires;

    public Car(string model, Engine engine, Cargo cargo, List<Tire> tires)
    {
        this.Model = model;
        this.engine = engine;
        this.cargo = cargo;
        this.tires = tires;
    }

    public string Model
    {
        get { return this.model; }
        set { this.model = value; }
    }
    public Engine Engine
    {
        get { return this.engine; }
        set { this.engine = value; }
    }

    public Cargo Cargo
    {
        get { return this.cargo; }
        set { this.cargo = value; }
    }

    public List<Tire> Tires
    {
        get { return this.tires; }
        private set { this.tires = value; }
    }
}

