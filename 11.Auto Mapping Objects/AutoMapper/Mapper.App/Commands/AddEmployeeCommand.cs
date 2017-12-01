using Mapper.DtoModels;
using Mapper.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    class AddEmployeeCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public AddEmployeeCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var decimalSalary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto(firstName,lastName,decimalSalary);
            employeeService.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} was successfully added!";
        }
    }
}
