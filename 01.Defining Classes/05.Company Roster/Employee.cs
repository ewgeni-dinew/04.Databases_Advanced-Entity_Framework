using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Employee
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public Employee()
    {
        this.Age = -1;
        this.Email = "n/a";
    }

    public Employee(string name, double salary, string position, string department)
        : this()
    {
        this.Name = name;
        this.Salary = salary;
        this.Position = position;
        this.Department = department;
    }

    public Employee(string name,double salary,string position,string department,string email)
        :this()
    {
        this.Email = email;
    }

    public Employee(string name, double salary, string position, string department,int age)
        : this()
    {
        this.Age = age;
    }
}


