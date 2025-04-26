using AutoMapper;
using ITI_Api.DTO.StudentDTO;
using ITI_Api.Models;

namespace ITI_Api.Mapping
{
    public class StudentMap:Profile
    {
        public StudentMap()
        {
            CreateMap<Student, ReadStudentDTO>().AfterMap((src, dest) =>
            {
                dest.FirstName = src.StFname;
                dest.LastName = src.StLname;
                dest.DepName = src.Dept?.DeptName;
            })
                    .ReverseMap();

        
        CreateMap<UpdateStudentDTO, Student>()
            .AfterMap((src, dest) =>
            {
            dest.StFname = src.FirstName;
            dest.StLname = src.LastName;
             dest.DeptId = src.DepID;
               

            }).ReverseMap();

            CreateMap<AddStudentDTO, Student>().AfterMap((src, dest) =>
                {
                    dest.StFname = src.FirstName;
                    dest.StLname = src.LastName;
                    dest.DeptId = src.DepId;

                })
                      .ReverseMap();


        }
}
}
