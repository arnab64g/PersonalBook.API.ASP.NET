using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;
using PersonalBook.API.Model;

namespace PersonalBook.API.Services
{
    public interface ISecondaryResultService
    {
        public Task<int> AddSecondaryResultAsync(SecondaryResult secondaryResult);
        public Task<AllSecondaryResults> GetResultAsync(Guid id);
        public Task<int> UpdateResultAsync(SecondaryResult secondaryResult);
        public Task<int> DeleteResultAsync(int id);
    }

    public class SecondaryResultService : ISecondaryResultService
    {
        private readonly UserDbContext userDbContext;

        public SecondaryResultService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddSecondaryResultAsync(SecondaryResult secondaryResult)
        {
            await userDbContext.SecondaryResults.AddAsync(secondaryResult);

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteResultAsync(int id)
        {
            SecondaryResult? secondaryResult = await userDbContext.SecondaryResults.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (secondaryResult != null)
            {
                userDbContext.Remove(secondaryResult);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<AllSecondaryResults> GetResultAsync(Guid id)
        {
            var res = await userDbContext.Results.ToListAsync();
            var Results = await (from sr in userDbContext.SecondaryResults.Where(r => r.UserId == id)
                                 join g in userDbContext.Grades on sr.GradeId equals g.Id
                                 select new
                                 {
                                     sr.Id,
                                     sr.Sl,
                                     sr.UserId,
                                     sr.Subject,
                                     sr.IsOptional,
                                     sr.GradeId,
                                     g.GradeName,
                                     g.Points,
                                     sr.Level,
                                 }).ToListAsync();

            var Summary = Results.GroupBy(g => g.Level).Select(x => new
                                {
                                    Level = x.Key,
                                    TotalPoints = x.Sum(a => a.Points),
                                    TotalSubjects = x.Count(),
                                }).ToList();
            
            AllSecondaryResults allSecondaryResults = new AllSecondaryResults
            {
                Results = Results.Select(d => new SecondaryResultTable
                            {
                                Id = d.Id,
                                Sl = d.Sl,
                                UserId = d.UserId,
                                Subject = d.Subject,
                                GradeId = d.GradeId,
                                IsOptional = d.IsOptional,
                                Level = d.Level,
                                GradeName = d.GradeName,
                                Points = d.Points,
                            }).ToList(),
                Summary = Summary.Select( d => new SecondarySummary
                            {
                                Level = d.Level,
                                TotalPoints = d.TotalPoints,
                                TotalSubjects = d.TotalSubjects,
                            }).ToList()
            };

            return allSecondaryResults;
        }

        public async Task<int> UpdateResultAsync(SecondaryResult secondaryResult)
        {
            SecondaryResult? secondaryResultOld = await userDbContext.SecondaryResults.Where(r => r.Id == secondaryResult.Id).FirstOrDefaultAsync();

            if (secondaryResultOld != null)
            {
                secondaryResultOld.Sl = secondaryResult.Sl;
                secondaryResultOld.Subject = secondaryResult.Subject;
                secondaryResultOld.GradeId = secondaryResult.GradeId;
                secondaryResultOld.IsOptional = secondaryResult.IsOptional;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
