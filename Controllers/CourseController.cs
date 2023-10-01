using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Model;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ICourseServive courseServive;

        public CourseController(ICourseServive courseServive)
        {
            this.courseServive = courseServive;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            int res = await courseServive.AddCourseAsync(course);

            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            List<Course> courses = await courseServive.GetCoursesAsync(id);

            return Ok(courses);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await courseServive.DeleteCourseAsync(id);

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Course course)
        {
            int res = await courseServive.UpdateCourseAsync(course);

            return Ok(res);
        }
    }
}
