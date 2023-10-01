using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Model;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;

        public GradeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] Grade grade)
        {
            int res = await gradeService.AddGradeAsync(grade);

            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await gradeService.DeleteGradeAsync(id);

            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            List<Grade> grades = await gradeService.GetGradesAsync();

            return Ok(grades);
        }

        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update([FromBody] Grade grade)
        {
            int res = await gradeService.UpdateGradeAsync(grade);

            return Ok(res);
        }
    }
}
