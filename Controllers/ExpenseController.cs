using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBook.API.Model;
using PersonalBook.API.Services;

namespace PersonalBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(Guid id) 
        {
            List<Expense> expenses = await expenseService.GetExpenseAsync(id);

            return Ok(expenses);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Expense expense)
        {
            int res = await expenseService.AddExpenseAsync(expense);

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Expense expense)
        {
            int res = await expenseService.UpdateExpenseAsync(expense);

            return Ok(res);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int res = await expenseService.DeleteExpenseAsync(id);

            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("Category")]
        public IActionResult Get([FromBody] CategorySummaryRequest categorySummaryRequest)
        {
            var Summary = expenseService.CategorySummaryAsync(categorySummaryRequest);

            return Ok(Summary);
        }
    }
}
