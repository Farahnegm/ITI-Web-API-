using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.DTO.StudentDTO;

namespace ITI_Api.Interfaces
{
     public interface IStudentService

    {
        int CountStudentsByDep(int DepId);
        DepartmentDTO StudentDeps(int StudentID);
            
    }
}
