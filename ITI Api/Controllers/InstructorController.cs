using AutoMapper;
using ITI_Api.DTO;
using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.DTO.InstructorDTO;
using ITI_Api.DTO.StudentDTO;
using ITI_Api.Extentions;
using ITI_Api.Models;
using ITI_Api.Repository.DepartmentRepository;
using ITI_Api.Repository.InstructorRepository;
using ITI_Api.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;

namespace ITI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        UnitOfWork unitOfWork;
        IMapper _mapper;

        public InstructorController(UnitOfWork unitOfWork, IMapper _mapper)
        {

            this._mapper = _mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page =1, int pageSize=10)
        {

            var InstructorQuery = unitOfWork.instructorRepo.GetAll();
            var pagedResult =await InstructorQuery.ToPagedResultAsync(page, pageSize);

            var mappedInstructor = _mapper.Map<List<ReadInstructorDTO>>(pagedResult.Items);


            var response = new PagedResultDTO<ReadInstructorDTO>
            {
                CurrentPage = pagedResult.CurrentPage,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages,
                TotalCount = pagedResult.TotalCount,
                Items = mappedInstructor
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var instructor = unitOfWork.instructorRepo.GetById(id);
            if (instructor == null)
                return NotFound();
            var res = _mapper.Map<ReadInstructorDTO>(instructor);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedIns = unitOfWork.instructorRepo.Delete(id);
            if (deletedIns == null)
                return NotFound();

            return NoContent();

        }
        [HttpPost]
        public IActionResult Add(AddInstructorDTO dto)
        {
            var instructor = _mapper.Map<Instructor>(dto);

            var added = unitOfWork.instructorRepo.Add(instructor);

            if (added == null)
                return BadRequest(new { message = "Invalid Department ID. Department not found." });

            
            var instructorDTO = _mapper.Map<ReadInstructorDTO>(added);

            return CreatedAtAction(nameof(GetById), new { id = added.InsId }, instructorDTO);
        }

       
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateInstructorDTO dto)
        {
            var existingIns = unitOfWork.instructorRepo.GetById(id);

            if (existingIns == null)
                return NotFound();

            _mapper.Map(dto, existingIns);

            unitOfWork.instructorRepo.Update(existingIns, id);
            return NoContent();
        }

    }
}
