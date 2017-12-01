using Mapper.Data;
using Mapper.DtoModels;
using Mapper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Mapper.Services
{
    public class EmployeeService
    {
        private readonly EmployeeContext context;

        public EmployeeService(EmployeeContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = context
                .Employees
                .Find(employeeId);

            var employeeDto = AutoMapper.Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = AutoMapper.Mapper.Map<Employee>(dto);
            context.Employees.Add(employee);

            context.SaveChanges();
        }

        public string SetBirthday(int employeeId,DateTime date)
        {
            var employee = context
                .Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();

            employee.Birthday = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context
                .Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();

            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = AutoMapper.Mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalDto SetManager(int employeeId,int managerId)
        {
            var employee = context
                .Employees
                .Find(employeeId);

            var manager = context
                .Employees
                .Find(managerId);

            employee.Manager = manager;
            context.SaveChanges();

            var employeePersonalDto = AutoMapper.Mapper.Map<EmployeePersonalDto>(employee);
            return employeePersonalDto;
        }

        public ManagerDto GetManager(int managerId)
        {
            var employee = context
                .Employees
                .Include(e=>e.ManagerEmployees)
                .SingleOrDefault(m=>m.Id==managerId);

            var managerDto = AutoMapper.Mapper.Map<ManagerDto>(employee);

            return managerDto;
        }

        public List<EmployeeManagerDto> OlderThan(int age)
        {
            var employees = context.Employees
                .Where(e => e.Birthday != null && Math.Floor((DateTime.Now - e.Birthday.Value).TotalDays / 365) > age)
                .Include(e => e.Manager)
                .ProjectTo<EmployeeManagerDto>()
                .ToList();

            return employees;
        }
    }
}
