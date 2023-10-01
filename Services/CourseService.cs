using PersonalBook.API.Data;
using PersonalBook.API.Model;
using Microsoft.EntityFrameworkCore;

namespace PersonalBook.API.Services
{
    public interface ICourseServive
    {
        public Task<int> AddCourseAsync(Course course);
        public Task<List<Course>> GetCoursesAsync(Guid userId);
        public Task<int> UpdateCourseAsync(Course course);
        public Task<int> DeleteCourseAsync(int id);
    }

    public class CourseService : ICourseServive
    {
        private readonly UserDbContext userDbContext;

        public CourseService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<int> AddCourseAsync(Course course)
        {
            await userDbContext.Courses.AddAsync(course);

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCourseAsync(int id)
        {
            Course? course = await userDbContext.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
            Result? res = await userDbContext.Results.Where(r => r.CourseId == id).FirstOrDefaultAsync();

            if (res != null)
            {
                return 0;
            }

            if (course != null)
            {
                userDbContext.Courses.Remove(course);
            }

            return await userDbContext.SaveChangesAsync();
        }

        public async Task<List<Course>> GetCoursesAsync(Guid userId)
        {
            return await userDbContext.Courses.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<int> UpdateCourseAsync(Course course)
        {
            Course? course1 = await userDbContext.Courses.Where(c => c.Id ==  course.Id).FirstOrDefaultAsync();

            if (course1 != null)
            {
                course1.CourseCode = course.CourseCode;
                course1.CourseTitle = course.CourseTitle;
                course1.CreditPoint = course.CreditPoint;
            }

            return await userDbContext.SaveChangesAsync();
        }
    }
}
