using Mapper.DtoModels;
using Mapper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mapper.App.Commands
{
    class EmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int age = int.Parse(args[0]);

            List<ManagedEmployeeDto> employees = employeeService.OlderThan(age);

            if (employees==null)
            {
                throw new ArgumentException($"No employees older than {age}.");
            }

            var result = "";

            foreach (var emp in employees.OrderByDescending(e=>e.Salary))
            {
                var manager = "";

                if (emp.Manager==null)
                {
                    manager = "[no manager]";
                }
                else
                {
                    manager = emp.Manager.LastName;
                }

                result += $"{emp.FirstName} {emp.LastName} - ${emp.Salary:f2} - " +
                    $"Manager: {manager}"+Environment.NewLine;
            }

            return result;
        }
    }
}
