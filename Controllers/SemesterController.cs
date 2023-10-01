using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PersonalBook.API.Model;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            this.semesterService = semesterService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            List<Semester> semesters = await semesterService.GetSemestersAsync(id);

            return Ok(semesters);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Semester semester)
        {
            int res = await semesterService.AddSemesterAsync(semester);

            return Ok(res);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await semesterService.DeleteSemesterAsync(id);

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Semester semester)
        {
            int res = await semesterService.UpdateSemesterAsync(semester);

            return Ok(res);
        }
    }
}
