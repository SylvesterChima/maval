using AutoMapper;
using Marval.Domain.Entities;
using Marval.Models;

namespace Marval.ObjectMapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
        }
    }
}
