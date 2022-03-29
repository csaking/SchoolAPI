using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolApiContext _context;
        public readonly IMapper _mapper;

        public StudentsController(SchoolApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("get/all")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                return Ok(await StudentService.GetAllStudentsAsync(_context));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("get/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var student = await StudentService.GetStudentByIdAsync(id, _context);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut, Route("edit")]
        public async Task<IActionResult> PutStudent(StudentModelIn model)
        {
            try
            {
                if (!(model.Id.HasValue && await StudentService.StudentExistsAsync(model.Id.Value, _context))) return NotFound();

                if (ValidateStudentModelIn(model))
                {
                    try
                    {
                        return Ok(await StudentService.UpdateStudentByIdAsync(model, _mapper, _context));

                    }
                    catch
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult<Student>> PostStudent(StudentModelIn model)
        {
            try 
            {
                if (ValidateStudentModelIn(model))
                {
                    var student = await StudentService.CreateStudentAsync(model, _mapper, _context);
                    return CreatedAtAction("GetStudent", new { id = student.Id }, student);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                if (!await StudentService.StudentExistsAsync(id, _context))
                {
                    return NotFound();
                }

                try
                {
                    return Ok(await StudentService.DeleteStudentByIdAsync(id, _context));
                }
                catch 
                { 
                    return NoContent(); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateStudentModelIn(StudentModelIn model)
        {
            if(model.Name.Length >= 1
                && model.Name.Length <= 50)
            {
                return true;
            }
            return false;
        }
    }
}
