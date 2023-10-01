using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;
using PersonalBook.API.Model;

namespace PersonalBook.API.Services
{
    public interface IResultService
    {
        public Task<int> AddresultAsync(Result result);
        public Task<dynamic> GetResultsAsync(Guid userId);
        public Task<int> DeleteResultAsync(int id);
        public Task<int> UpdateResultAsync(Result result);
    }

    public class ResultService : IResultService
    {
        private readonly UserDbContext userDbContext;

        public ResultService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddresultAsync(Result result)
        {
            await userDbContext.Results.AddAsync(result);

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteResultAsync(int id)
        {
            Result? res = await userDbContext.Results.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (res != null)
            {
                userDbContext.Results.Remove(res);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<dynamic> GetResultsAsync(Guid userId)
        {
            var Results = await (from r in userDbContext.Results.Where(r => r.UserId == userId)
                                 join c in userDbContext.Courses on r.CourseId equals c.Id
                                 join s in userDbContext.Semesters on r.SemesterId equals s.Id
                                 join g in userDbContext.Grades on r.GradeId equals g.Id
                                 select new
                                 {
                                     r.Id,
                                     r.SemesterId,
                                     s.SemesterName,
                                     s.MonthBng,
                                     s.Year,
                                     c.CourseCode,
                                     c.CourseTitle,
                                     g.GradeName,
                                     g.Points,
                                     c.CreditPoint,
                                     r.GradeId,
                                     r.CourseId
                                 }
                             ).ToListAsync();
            var Summary = Results.GroupBy(x => x.SemesterId).Select(x => new
            {
                SemId = x.Key,

                TotalPoints = x.Sum(a => a.Points * a.CreditPoint),
                TotalCredit = x.Sum(a => a.CreditPoint)
            }).OrderBy(s => s.SemId);
            var TotalPoints = Results.Sum(r => r.Points * r.CreditPoint);
            var TotalCredit = Results.Sum(r => r.CreditPoint);

            return new { Results, Summary, TotalPoints, TotalCredit };
        }

        public async Task<int> UpdateResultAsync(Result result)
        {
            Result? oldRes = await userDbContext.Results.Where(r => r.Id == result.Id).FirstOrDefaultAsync();

            if (oldRes != null)
            {
                oldRes.SemesterId = result.SemesterId;
                oldRes.CourseId = result.CourseId;
                oldRes.GradeId = result.GradeId;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
