using PersonalBook.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondaryResultController : Controller
    {
        private readonly ISecondaryResultService secondaryResultService;

        public SecondaryResultController(ISecondaryResultService secondaryResultService)
        {
            this.secondaryResultService = secondaryResultService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] SecondaryResult secondaryResult)
        {
            int res = await secondaryResultService.AddSecondaryResultAsync(secondaryResult);

            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            AllSecondaryResults res = await secondaryResultService.GetResultAsync(id);

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] SecondaryResult secondaryResult)
        {
            int res = await secondaryResultService.UpdateResultAsync(secondaryResult);

            return Ok(res);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await secondaryResultService.DeleteResultAsync(id);

            return Ok(res);
        }
    }
}
