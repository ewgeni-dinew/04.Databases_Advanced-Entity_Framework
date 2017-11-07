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
        var departmentList = new Dictionary<string, double>();
        var employeeList = new List<Employee>();

        for (int i = 0; i < inputLines; i++)
        {
            var line = Console.ReadLine()
                .Split(' ');

            var currentName = line[0];
            var currentSalary = double.Parse(line[1]);
            var currentPos = line[2];
            var currentDep = line[3];

            var currentEmployee = new Employee(currentName, currentSalary, currentPos, currentDep);

            //Find Dep with highest AVG(SALARY)
            if (!departmentList.ContainsKey(currentDep))
            {
                departmentList[currentDep] = 0;
            }

            departmentList[currentDep] += (currentSalary/(double)inputLines);

            //Find employee info
            if (line.Length==4)
            {
                employeeList.Add(currentEmployee);
            }
            else if (line.Length==5)
            {
                var isAge = int.TryParse(line[4], out int age);
                if (isAge)
                {
                    currentEmployee.Age = age;
                }
                else
                {
                    currentEmployee.Email = line[4];
                }
                employeeList.Add(currentEmployee);
            }
            else
            {
                currentEmployee.Email = line[4];
                currentEmployee.Age = int.Parse(line[5]);
                employeeList.Add(currentEmployee);
            }
        }

        var highestPaidDep = departmentList.OrderByDescending(v => v.Value).First();

        Console.WriteLine($"Highest Average Salary: {highestPaidDep.Key}");

        foreach (var emp in employeeList
            .Where(e=>e.Department==highestPaidDep.Key)
            .OrderByDescending(e=>e.Salary))
        {
            Console.WriteLine($"{emp.Name} {emp.Salary:f2} {emp.Email} {emp.Age}");
        }
    }
}

