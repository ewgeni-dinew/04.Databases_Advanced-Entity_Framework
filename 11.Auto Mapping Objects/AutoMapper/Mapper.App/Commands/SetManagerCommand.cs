using Mapper.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetManagerCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var managerId = int.Parse(args[1]);

            var employeePersDto = employeeService.SetManager(employeeId, managerId);

            return $"{employeePersDto.Manager.FirstName} {employeePersDto.Manager.LastName} " +
                $"is successfuly added as a manager to " +
                $"{employeePersDto.FirstName} {employeePersDto.LastName}";
        }
    }
}
