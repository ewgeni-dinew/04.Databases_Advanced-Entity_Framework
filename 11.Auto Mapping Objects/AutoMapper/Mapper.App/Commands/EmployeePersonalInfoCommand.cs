using Mapper.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var employee = employeeService.PersonalById(employeeId);
            var birthday = "No birthday specified";

            if (employee.Birthday!=null)
            {
                birthday = employee.Birthday.Value.ToString("dd-MM-yyyy");
            }

            var address = employee.Address?? "No address specified";


            var result =$"ID: {employeeId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}"
                +Environment.NewLine+$"Birthday: {birthday}"+Environment.NewLine
                +$"Address: {address}";

            return result;
        }
    }
}
