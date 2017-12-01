using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.DtoModels
{
    public class ManagerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<EmployeeDto> ManagedEmployees { get; set; }
        public int ManagerEmployeesCount { get; set; }
    }
}
