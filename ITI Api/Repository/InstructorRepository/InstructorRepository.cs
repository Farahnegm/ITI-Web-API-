using ITI_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Repository.InstructorRepository
{
    public class InstructorRepository : GenericRepository<Instructor>
    {
        public InstructorRepository(ITIContext db) : base(db)
        {
        }

        public IQueryable<Instructor> GetAll()
        {
            return _db.Instructors.Include(d => d.Departments);

        }
        public new Instructor GetById(int id)
        {
            var Ins = _db.Instructors.Include(d => d.Departments).FirstOrDefault(d => d.InsId == id);
            return Ins;
        }

        public new Instructor? Add(Instructor instructor)
        {
            var dep = _db.Departments.FirstOrDefault(i => i.DeptId == instructor.DeptId);

            if (dep == null)
                return null;

            _db.Instructors.Add(instructor);
            _db.SaveChanges();

            return instructor;
        }


        public new void Update(Instructor instructor, int id)
        {
            var existingInstructor = _db.Instructors.Find(id);
            if (existingInstructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {id} not found.");
            }

            if (instructor.DeptId.HasValue)
            {
                var DeptExist = _db.Departments.Any(i => i.DeptId == instructor.DeptId);
                if (!DeptExist)
                {
                    throw new ArgumentException("Department not found");
                }
            }

            _db.Entry(existingInstructor).CurrentValues.SetValues(instructor);
            _db.SaveChanges();
        }

        public new Instructor Delete(int id)
        {
            var ins = GetById(id);
            base.Delete(id);
            return ins;
        }
        public new void Save()
        {
            _db.SaveChanges();
        }
    }
}

