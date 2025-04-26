using ITI_Api.Models;

namespace ITI_Api.Interfaces
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        Task<Student> GetById(int id);
         IQueryable<Student> GetAll();
        Task<Student> AddStudent(Student student);
        Task<Student> Update(Student student, int id);
        Task<Student> Delete(int id);
    }
}
