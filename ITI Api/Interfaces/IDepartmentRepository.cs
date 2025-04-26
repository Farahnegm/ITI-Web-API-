using ITI_Api.Models;

namespace ITI_Api.Interfaces
{
    public interface IDepartmentRepository: IGenericRepository<Department>
    {
        Task<Department> GetById(int id);
        IQueryable<Department> GetAll();
        Task<Department> AddDepartment(Department department);
        Task  Update(Department department, int id);
        Task<Department> Delete(int id);
    }
}
