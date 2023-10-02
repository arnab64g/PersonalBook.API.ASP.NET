using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;
using PersonalBook.API.Model;

namespace PersonalBook.API.Services
{
    public interface IExpenseService
    {
        public Task<int> AddExpenseAsync(Expense expense);
        public Task<int> DeleteExpenseAsync(int id);
        public Task<List<Expense>> GetExpenseAsync(Guid userId);
        public Task<int> UpdateExpenseAsync(Expense expense);
        public dynamic CategorySummaryAsync(CategorySummaryRequest categorySummaryRequest);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly UserDbContext userDbContext;

        public ExpenseService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddExpenseAsync(Expense expense)
        {
            expense.Date = new DateTime(expense.Date.Year, expense.Date.Month, expense.Date.Day, 12, 0, 0);
            await userDbContext.Expenses.AddAsync(expense);

            return await userDbContext.SaveChangesAsync();
        }

        public dynamic CategorySummaryAsync(CategorySummaryRequest categorySummaryRequest)
        {
            if (categorySummaryRequest.FromDate == null && categorySummaryRequest.ToDate ==null)
            {
                var Summary = userDbContext.Expenses.Where(e => e.UserId == categorySummaryRequest.UserId).GroupBy(e => e.Category).Select(x => new
                {
                    Category = x.Key,
                    Total = x.Sum(x => x.Amount)
                }).OrderBy(x => x.Category);

                return Summary;
            }
            else if(categorySummaryRequest.FromDate == null)
            {
                var Summary = userDbContext.Expenses
                    .Where(e => e.UserId == categorySummaryRequest.UserId && e.Date <= categorySummaryRequest.ToDate)
                    .GroupBy(e => e.Category).Select(x => new
                {
                    Category = x.Key,
                    Total = x.Sum(x => x.Amount)
                }).OrderBy(x => x.Category);

                return Summary;
            }
            else if(categorySummaryRequest.ToDate == null)
            {
                var Summary = userDbContext.Expenses
                    .Where(e => e.UserId == categorySummaryRequest.UserId && e.Date >= categorySummaryRequest.FromDate)
                    .GroupBy(e => e.Category).Select(x => new
                {
                    Category = x.Key,
                    Total = x.Sum(x => x.Amount)
                }).OrderBy(x => x.Category);

                return Summary;
            }
            else
            {
                var Summary = userDbContext.Expenses
                    .Where(e => e.UserId == categorySummaryRequest.UserId && e.Date >= categorySummaryRequest.FromDate && e.Date <= categorySummaryRequest.ToDate)
                    .GroupBy(e => e.Category).Select(x => new
                {
                    Category = x.Key,
                    Total = x.Sum(x => x.Amount)
                }).OrderBy(x => x.Category);

                return Summary;
            }
        }

        public async Task<int> DeleteExpenseAsync(int id)
        {
            Expense? expense = await userDbContext.Expenses.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (expense != null)
            {
                userDbContext.Expenses.Remove(expense);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<List<Expense>> GetExpenseAsync(Guid userId)
        {
            List<Expense> expenses = await userDbContext.Expenses.Where(e=>e.UserId == userId).ToListAsync();

            return expenses;
        }

        public async Task<int> UpdateExpenseAsync(Expense expense)
        {
            Expense? expenseOld = await userDbContext.Expenses.Where(e => e.Id == expense.Id).FirstOrDefaultAsync();
            expense.Date = new DateTime(expense.Date.Year, expense.Date.Month, expense.Date.Day, 12, 0, 0);

            if (expenseOld != null)
            {
                expenseOld.Category = expense.Category;
                expenseOld.Date = expense.Date;
                expenseOld.Description = expense.Description;
                expenseOld.Amount = expense.Amount;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
