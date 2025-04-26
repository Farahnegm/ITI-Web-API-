using ITI_Api.Interfaces;
using ITI_Api.Repository;
using ITI_Api.Services;
using ITI_Api.Models;
using System.Threading.Tasks;
using ITI_Api.Repository.StudentRepository;
using ITI_Api.Repository.DepartmentRepository;
using ITI_Api.Repository.InstructorRepository;
using Microsoft.Identity.Client;

namespace ITI_Api.UnitOfWorks
{
    public class UnitOfWork 
    {
        private readonly ITIContext _db;

        IStudentRepository _studentRepo;
        IDepartmentRepository _departmentRepo;

        IGenericRepository<Instructor> _instructorRepo;

         IStudentService _studentService;

        public UnitOfWork(ITIContext db)
        {
            _db = db;
            
        }

        public IStudentRepository StudentRepo
        {
            get
            {
                if (_studentRepo == null)
                    _studentRepo = new StudentRepository(_db);
                return _studentRepo;
            }
        }
        public IGenericRepository<Instructor> instructorRepo
        {
            get
            {
                if (_instructorRepo == null)
                    _instructorRepo = new InstructorRepository(_db);
                return _instructorRepo;
            }
        }
        public IDepartmentRepository departmentRepo
        {
            get
            {
                if (_departmentRepo == null)
                    _departmentRepo = new DepartmentRepository(_db);
                return _departmentRepo;
            }
        }
        public IStudentService studentService
        {
            get
            {
                if (_studentService == null)
                    _studentService = new studentService(_db);
                return _studentService;
            }
        }



        

        
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
