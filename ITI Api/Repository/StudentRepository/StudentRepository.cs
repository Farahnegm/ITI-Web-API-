using ITI_Api.Interfaces;
using ITI_Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Repository.StudentRepository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ITIContext db) : base(db)
        {

        }
        public  IQueryable<Student> GetAll()
        {
            return _db.Students.Include(s => s.Dept);
        }
        public async Task <Student> GetById(int id)
        {
            var student = await _db.Students.Include(s => s.Dept).FirstOrDefaultAsync(s => s.StId == id); 
            return student;
        }
        public async Task <Student> Update(Student s,int id)
        {
            var student =await _db.Students
                .Include(s => s.Dept)
               . FirstOrDefaultAsync(s => s.StId == id);
            if (s.StSuper != null && await _db.Students.AnyAsync(st => st.StId == s.StSuper))
            {
                student.StSuper = s.StSuper; 
            }
            else
            {
                student.StSuper = null;
            }
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task <Student> Delete(int id)
        {
            var student = await GetById(id);
            base.Delete(id);
            return  student;
        }

        public async Task<Student> AddStudent(Student student)
        {
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            return student;
        }




    }
}
