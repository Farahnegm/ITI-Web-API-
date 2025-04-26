using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.DTO.StudentDTO;
using ITI_Api.Interfaces;
using ITI_Api.Models;

namespace ITI_Api.Services
{
    public class studentService : IStudentService

    {
        protected ITIContext _db;

        public studentService(ITIContext db)
        {
            _db = db;
        }
        public int CountStudentsByDep(int DepId)
        {
            var studentCount = _db.Students
                .Where(s => s.DeptId == DepId)
                .Count();
            return studentCount;
        }

        public DepartmentDTO StudentDeps(int StudentID)
        {
            var dep = _db.Students
                .Where(s => s.StId == StudentID)
                .Select(s => new DepartmentDTO
                {
                    DeptId =  s.DeptId ?? 0,
                    DeptName = s.Dept.DeptName,

                }).FirstOrDefault();
            return dep;
        }
    }
}
