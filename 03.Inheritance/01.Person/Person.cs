using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string name;
    private int age;

    public Person(string name, int age)
    {
        this.Age = age;
        this.Name = name;
    }

    public virtual string Name
    {
        get { return this.name; }
        set
        {
            if (value.Length<3)
            {
                throw new ArgumentException("Name's length should not be less than 3 symbols!");
            }
            this.name = value;
        }
    }

    public virtual int Age
    {
        get { return this.age; }
        set
        {
            if (value <0)
            {
                throw new ArgumentException("Age must be positive!");
            }
            this.age = value;
        }
    }

    public override string ToString()
    {
        return $"Name: {this.Name}, Age: {this.Age}";
    }
}

