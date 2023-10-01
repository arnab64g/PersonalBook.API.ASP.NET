using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Data;
using PersonalBook.API.Model;

namespace PersonalBook.API.Services
{
    public interface ISemesterService
    {
        public Task<int> AddSemesterAsync(Semester semester);
        public Task<List<Semester>> GetSemestersAsync(Guid id);
        public Task<int> DeleteSemesterAsync(int id);
        public Task<int> UpdateSemesterAsync(Semester semester);
    }

    public class SemesterService : ISemesterService
    {
        private readonly UserDbContext userDbContext;

        public SemesterService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddSemesterAsync(Semester semester)
        {
            await userDbContext.Semesters.AddAsync(semester);

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteSemesterAsync(int id)
        {
            Result? res = await userDbContext.Results.Where(r => r.SemesterId == id).FirstOrDefaultAsync();

            if (res != null)
            {
                return 0;
            }

            Semester? semester = await userDbContext.Semesters.Where(d => d.Id == id).FirstOrDefaultAsync();

            if (semester != null)
            {
                userDbContext.Remove(semester);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<List<Semester>> GetSemestersAsync(Guid id)
        {
            return await userDbContext.Semesters.Where(s => s.UserId == id).ToListAsync();
        }

        public async Task<int> UpdateSemesterAsync(Semester semester)
        {
            Semester? semesterOld = await userDbContext.Semesters.Where(d => d.Id == semester.Id).FirstOrDefaultAsync();

            if (semesterOld != null)
            {
                semesterOld.SemesterName = semester.SemesterName;
                semesterOld.MonthBng = semester.MonthBng;
                semesterOld.Year = semester.Year;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
