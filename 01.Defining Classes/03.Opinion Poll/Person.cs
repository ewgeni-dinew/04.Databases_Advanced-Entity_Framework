using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person()
    {
        this.Name = "No name";
        this.Age = 1;
    }

    public Person(int number)
        : this()
    {
        this.Age = number;
    }

    public Person(string name, int age)
        : this()
    {
        this.Name = name;
        this.Age = age;
    }
}