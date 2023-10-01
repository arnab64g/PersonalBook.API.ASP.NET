using PersonalBook.API.Model;
using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;

namespace PersonalBook.API.Services
{
    public interface IGradeService
    {
        public Task<List<Grade>> GetGradesAsync();
        public Task<int> AddGradeAsync(Grade grade);
        public Task<int> UpdateGradeAsync(Grade grade);
        public Task<int> DeleteGradeAsync(int id);
    }

    public class GradeService : IGradeService
    {
        private readonly UserDbContext userDbContext;

        public GradeService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddGradeAsync(Grade grade)
        {
            grade.GradeName = grade?.GradeName?.ToUpper();
            await userDbContext.Grades.AddAsync(grade);

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteGradeAsync(int id)
        {
            Result? res = await userDbContext.Results.Where(r => r.GradeId == id).FirstOrDefaultAsync();
            SecondaryResult? secondaryResult = await userDbContext.SecondaryResults.Where(r => r.GradeId == id).FirstOrDefaultAsync();

            if (res != null || secondaryResult != null)
            {
                return 0;
            }

            Grade? grade = await userDbContext.Grades.Where(grade => grade.Id == id).FirstOrDefaultAsync();

            if (grade != null)
            {
                userDbContext.Remove(grade);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<List<Grade>> GetGradesAsync()
        {
            return await userDbContext.Grades.ToListAsync();
        }

        public async Task<int> UpdateGradeAsync(Grade grade)
        {
            grade.GradeName = grade?.GradeName?.ToUpper();
            Grade? oldGrdade = await userDbContext.Grades.Where(g => g.Id == grade.Id).FirstOrDefaultAsync();

            if (oldGrdade != null)
            {
                oldGrdade.GradeName = grade.GradeName;
                oldGrdade.Points = grade.Points;
                oldGrdade.MinNumber = grade.MinNumber;
                oldGrdade.MaxNumber = grade.MaxNumber;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
