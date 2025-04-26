using AutoMapper;
using ITI_Api.DTO;
using ITI_Api.DTO.DepartmentDTO;
using ITI_Api.Extentions;
using ITI_Api.Models;
using ITI_Api.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var depQuery = unitOfWork.departmentRepo.GetAll();
            var pagedResult = await depQuery.ToPagedResultAsync(page, pageSize);
            var mappedItems = _mapper.Map<List<ReadDepartmentDTO>>(pagedResult.Items);

            var response = new PagedResultDTO<ReadDepartmentDTO>
            {
                CurrentPage = pagedResult.CurrentPage,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages,
                TotalCount = pagedResult.TotalCount,
                Items = mappedItems
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await unitOfWork.departmentRepo.GetById(id);

            if (department == null)
                return NotFound();

            var res = _mapper.Map<ReadDepartmentDTO>(department);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedDepartment = await unitOfWork.departmentRepo.Delete(id);

            if (deletedDepartment == null)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentDTO dto)
        {
            var dep = _mapper.Map<Department>(dto);

            var added = await unitOfWork.departmentRepo.AddDepartment(dep);

            if (added == null)
                return BadRequest(new { message = "Invalid DeptManager ID. Instructor not found." });

            return CreatedAtAction(nameof(GetById), new { id = added.DeptId }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDTO dto)
        {
            var existingDep = await unitOfWork.departmentRepo.GetById(id);

            if (existingDep == null)
                return NotFound();

            _mapper.Map(dto, existingDep);

            await unitOfWork.departmentRepo.Update(existingDep, id);
            return NoContent();
        }
    }
}
