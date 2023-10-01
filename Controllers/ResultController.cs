using PersonalBook.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        private readonly IResultService resultService;

        public ResultController(IResultService resultService)
        {
            this.resultService = resultService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Result result)
        {
            int res = await resultService.AddresultAsync(result);

            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await resultService.GetResultsAsync(id);

            return Ok(res);
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            int res = await resultService.DeleteResultAsync(id);

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Result result)
        {
            int res = await resultService.UpdateResultAsync(result);

            return Ok(res);
        }
    }
}
