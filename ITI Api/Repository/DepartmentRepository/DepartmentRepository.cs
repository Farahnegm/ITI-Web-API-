using ITI_Api.Interfaces;
using ITI_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Repository.DepartmentRepository
{
    public class DepartmentRepository:GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ITIContext db) : base(db)
        {

        }
        public IQueryable<Department> GetAll()
        {
            return _db.Departments.Include(d => d.Instructors);
        }

        public async Task<Department> GetById(int id)
        {
            var department = await _db.Departments.Include(d => d.Instructors).FirstOrDefaultAsync(d => d.DeptId == id);
            return department;
        }


        public async Task <Department?> AddDepartment(Department department)
        {
            var ins = await _db.Instructors.FirstOrDefaultAsync(i => i.InsId == department.DeptManager);

            if (ins == null)
                return null;

            _db.Departments.Add(department);
            await _db.SaveChangesAsync();

            return department;
        }


        public async Task Update(Department department, int id)
        {
            var existingDepartment = await _db.Departments.FindAsync(id);

            if (existingDepartment == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            if (department.DeptManager.HasValue)
            {
                var managerExists = await _db.Instructors.AnyAsync(i => i.InsId == department.DeptManager);
                if (!managerExists)
                {
                    throw new ArgumentException("Instructor (manager) not found");
                }
            }

            _db.Entry(existingDepartment).CurrentValues.SetValues(department);
            await _db.SaveChangesAsync();

        }

        public  async Task<Department?> Delete(int id)
        {
            var dep = await GetById(id);
            if (dep == null) return null;

            base.Delete(id); 
            await _db.SaveChangesAsync(); 
            return dep;
        }

        public  void Save()
        {
            _db.SaveChanges();
        }
    }
    
    }

