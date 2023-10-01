using Microsoft.EntityFrameworkCore;
using PersonalBook.API.Model;

namespace PersonalBook.API.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SecondaryResult> SecondaryResults { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasKey(m => new {m.Id, m.Email,m.Username, m.PhoneNumber });
        }
    }
}
