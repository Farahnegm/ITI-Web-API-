using AutoMapper;
using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.DTO.InstructorDTO;
using ITI_Api.Models;

namespace ITI_Api.Mapping
{
    public class InstructorMap : Profile
    {
        public InstructorMap()
        {
            CreateMap<Instructor, ReadInstructorDTO>()
      .AfterMap((src, dest) =>
      {
          if (src.Dept != null)
          {
              dest.DeptName = src.Dept.DeptName;
          }
      });


            CreateMap<Instructor, AddInstructorDTO>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDTO>().ReverseMap();
        }
    }
    }



