using AutoMapper;
using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.Models;

namespace ITI_Api.Mapping
{
    public class DepartmentMap: Profile
    {
        public DepartmentMap()
        {
            CreateMap<Department,ReadDepartmentDTO>().AfterMap((src, dest)=>
            {
                Console.WriteLine("Instructors Count: " + (src.Instructors != null ? src.Instructors.Count() : 0));
                var manager = src.Instructors.Where(i=>i.InsId == src.DeptManager).FirstOrDefault();
                if (manager != null)
                    {
                        dest.DeptManagerName = manager.InsName;
                    }
                })
                .ReverseMap();
            CreateMap<Department, AddDepartmentDTO>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();


        }
    }
    
    }

