using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.DtoModels
{
    public class ManagedEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public ManagerDto Manager { get; set; }
    }
}
