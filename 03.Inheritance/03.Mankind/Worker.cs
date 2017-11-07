using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Worker:Human
{
    private decimal weekSalary;
    private double workHoursPerDay;

    public Worker(string firstName, string lastName,decimal weekSalary,double workHoursPerDay)
        : base(firstName, lastName)
    {
        this.WeekSalary = weekSalary;
        this.WorkHoursPerDay = workHoursPerDay;
    }

    public decimal WeekSalary
    {
        get {return this.weekSalary;}

        set
        {
            if (value <=10)
            {
                throw new ArgumentException($"Expected value mismatch!Argument: weekSalary");
            }

            this.weekSalary = value;
        }
    }

    public double WorkHoursPerDay
    {
        get
        {
            return this.workHoursPerDay;
        }

        private set
        {
            if (value <1 || value >12)
            {
                throw new ArgumentException($"Expected value mismatch! Argument: workHoursPerDay");
            }

            this.workHoursPerDay = value;
        }
    }

    private decimal CalcSalaryPerHour()
    {
        decimal result = this.WeekSalary / (decimal)(5 * this.WorkHoursPerDay);
        return result;
    }

    public override string ToString()
    {
        return $@"First Name: {this.FirstName}
Last Name: {this.LastName}
Week Salary: {this.WeekSalary:f2}
Hours per day: {this.WorkHoursPerDay:f2}
Salary per hour: {(CalcSalaryPerHour()):f2}";

    }
}

