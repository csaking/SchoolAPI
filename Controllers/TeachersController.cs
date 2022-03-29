using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly SchoolApiContext _context;
        public readonly IMapper _mapper;

        public TeachersController(SchoolApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("get/all")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            try
            {
                return Ok(await TeacherService.GetAllTeachersAsync(_context));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("get/{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            try
            {
                var teacher = await TeacherService.GetTeacherByIdAsync(id, _context);

                if (teacher == null)
                {
                    return NotFound();
                }

                return teacher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut, Route("edit")]
        public async Task<IActionResult> PutTeacher(TeacherModelIn model)
        {
            try
            {
                if (!(model.Id.HasValue && await TeacherService.TeacherExistsAsync(model.Id.Value, _context))) return NotFound();

                if (ValidateTeacherModelIn(model))
                {
                    try
                    {
                        return Ok(await TeacherService.UpdateTeacherByIdAsync(model, _mapper, _context));

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
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherModelIn model)
        {
            try
            {
                if (ValidateTeacherModelIn(model))
                {
                    var teacher = await TeacherService.CreateTeacherAsync(model, _mapper, _context);
                    return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
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
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                if (!await TeacherService.TeacherExistsAsync(id, _context))
                {
                    return NotFound();
                }

                try
                {
                    return Ok(await TeacherService.DeleteTeacherByIdAsync(id, _context));
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

        [HttpPut, Route("assign/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Class>>> AssignClass(int teacherId, int[] classIds)
        {
            try
            {
                if (!await TeacherService.TeacherExistsAsync(teacherId, _context)) return NotFound();

                foreach(var classId in classIds)
                {
                    if (!await ClassService.ClassExistsAsync(classId, _context)) return NotFound();
                }

                var currentClasses = await ClassService.GetClassesByTeacherIdAsync(teacherId, _context);

                var teacher = await TeacherService.GetTeacherByIdAsync(teacherId, _context);

                //Business Rule Validation: A Teacher cannot be assigned to more than 5 classes
                if (currentClasses.Count() + classIds.Length >= 5) return BadRequest();

                var newClasses = await ClassService.GetClassesByIdArrayAsync(classIds, _context);

                newClasses = await ClassService.AssignClassesToTeacherAsync(teacher, newClasses, _context);

                return Ok(newClasses);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateTeacherModelIn(TeacherModelIn model)
        {
            if (model.Name.Length >= 1
                && model.Name.Length <= 50)
            {
                return true;
            }
            return false;
        }
    }
}
