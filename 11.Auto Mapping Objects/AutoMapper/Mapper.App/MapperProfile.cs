using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App
{
    using AutoMapper;
    using Mapper.DtoModels;
    using Mapper.Models;

    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Employee, EmployeePersonalDto>();

            CreateMap<Employee, ManagerDto>();
        }
    }
}
