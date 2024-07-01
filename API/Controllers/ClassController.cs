using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;

        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            var classes = await _classRepository.GetClassesAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var classObj = await _classRepository.GetClassAsync(id);
            if (classObj == null)
            {
                return NotFound();
            }
            return Ok(classObj);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass(Class classObj)
        {
            await _classRepository.AddClassAsync(classObj);
            return CreatedAtAction(nameof(GetClass), new { id = classObj.Id }, classObj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, Class classObj)
        {
            if (id != classObj.Id)
            {
                return BadRequest();
            }

            await _classRepository.UpdateClassAsync(classObj);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            await _classRepository.DeleteClassAsync(id);
            return NoContent();
        }
    }
}
