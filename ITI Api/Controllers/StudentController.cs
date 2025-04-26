using AutoMapper;
using ITI_Api.DTO.StudentDTO;
using ITI_Api.Models;
using ITI_Api.Repository.StudentRepository;
using ITI_Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ITI_Api.UnitOfWorks;
using ITI_Api.DTO;
using ITI_Api.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        UnitOfWork unitOfWork;
        IMapper _mapper;
       
        public StudentController(UnitOfWork unitOfWork, IMapper _mapper)
        {
           
            this._mapper = _mapper;
            this.unitOfWork = unitOfWork;


        }
      [HttpGet]
public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
{
    var studentsQuery = unitOfWork.StudentRepo.GetAll();

  
    var pagedResult = await studentsQuery.ToPagedResultAsync(page, pageSize); 

    var mappedStudents = _mapper.Map<List<ReadStudentDTO>>(pagedResult.Items);

    var response = new PagedResultDTO<ReadStudentDTO>
    {
        CurrentPage = pagedResult.CurrentPage,
        PageSize = pagedResult.PageSize,
        TotalPages = pagedResult.TotalPages,
        TotalCount = pagedResult.TotalCount,
        Items = mappedStudents
    };

    return Ok(response);
}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await unitOfWork.StudentRepo.GetById(id);
            if (student == null) return NotFound();

            var res = _mapper.Map<ReadStudentDTO>(student);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDTO dto)
        {
            var existingStudent = await unitOfWork.StudentRepo.GetById(id);
            if (existingStudent == null) return NotFound();

            _mapper.Map(dto, existingStudent);
            await unitOfWork.SaveAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedStudent = await unitOfWork.StudentRepo.Delete(id);
            if (deletedStudent == null)
                return NotFound();

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddStudentDTO dto)
        {
            var student = _mapper.Map<Student>(dto);

            var added = await unitOfWork.StudentRepo.AddStudent(student);

            var res = _mapper.Map<ReadStudentDTO>(added);
            return Ok(res);
        }

        [HttpGet("CountStudentsByDepartment/{id}")]
        public IActionResult CountStudentsByDepartment(int id)
        {
            var count = unitOfWork.studentService.CountStudentsByDep(id);
            return Ok(count);
        }
        [HttpGet("findDepartmenBystudentID/{id}")]
        public IActionResult findDepartmenBystudentID(int id)
        {
            var dep = unitOfWork.studentService.StudentDeps(id);
            if (dep == null)
                return NotFound();
            return Ok(dep);
        }

    }
}
