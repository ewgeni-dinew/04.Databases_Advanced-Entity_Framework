using Mapper.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    public class ManagerInfoCommand:ICommand
    {
        private readonly EmployeeService employeeService;

        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var managerId = int.Parse(args[0]);

            var managerDto = employeeService.GetManager(managerId);

            Console.WriteLine($"{managerDto.FirstName} {managerDto.LastName} | " +
                $"Employees: {managerDto.ManagedEmployees.Count}"+Environment.NewLine);

            var result = "";

            foreach (var emp in managerDto.ManagedEmployees)
            {
                result+=($" -> {emp.FirstName} {emp.LastName} - " +
                    $"{emp.Salary:f2}"+Environment.NewLine);
            }
            return result;
        }
    }
}
