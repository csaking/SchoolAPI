using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly SchoolApiContext _context;
        public readonly IMapper _mapper;

        public ClassesController(SchoolApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("get/all")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasss()
        {
            try
            {
                return Ok(await ClassService.GetAllClasssAsync(_context));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("get/{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            try
            {
                var classroom = await ClassService.GetClassByIdAsync(id, _context);

                if (classroom == null)
                {
                    return NotFound();
                }

                return classroom;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut, Route("edit")]
        public async Task<IActionResult> PutClass(ClassModelIn model)
        {
            try
            {
                if (!(model.Id.HasValue && await ClassService.ClassExistsAsync(model.Id.Value, _context))) return NotFound();

                if (ValidateClassModelIn(model))
                {
                    try
                    {
                        return Ok(await ClassService.UpdateClassByIdAsync(model, _mapper, _context));
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult<Class>> PostClass(ClassModelIn model)
        {
            try
            {
                if (ValidateClassModelIn(model))
                {
                    var classroom = await ClassService.CreateClassAsync(model, _mapper, _context);
                    return CreatedAtAction("GetClass", new { id = classroom.Id }, classroom);
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
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                if (!await ClassService.ClassExistsAsync(id, _context))
                {
                    return NotFound();
                }

                try
                {
                    return Ok(await ClassService.DeleteClassByIdAsync(id, _context));
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

        [HttpPut, Route("enrol")]
        public async Task<IActionResult> EnrolStudents(int classId, int[] studentIds)
        {
            try
            {
                if(!(await ClassService.ClassExistsAsync(classId, _context))) return NotFound();

                foreach (var studentId in studentIds)
                {
                    if (!await StudentService.StudentExistsAsync(studentId, _context)) return NotFound();
                }

                var classroom = await ClassService.GetClassByIdAsync(classId, _context);

                var studentsInClass = await StudentService.GetStudentsByClassIdAsync(classId, _context);

                //Business Rule Validation: A Student(s) cannot be enrolled in a Class that would put that Class over capacity
                if ((studentsInClass.Count() + studentIds.Length) > classroom.Capacity) return BadRequest();

                var newStudents = await StudentService.GetStudentsByIdArrayAsync(studentIds, _context);

                return Ok(await ClassService.EnrolStudentsInClassAsync(classroom, newStudents, _context));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateClassModelIn(ClassModelIn model)
        {
            if(model.Capacity >= 5 
                && model.Capacity <= 30 
                && model.Name.Length <= 20 
                && model.Name.Length >= 1)
            {
                return true;
            }
            return false;
        }
    }
}
