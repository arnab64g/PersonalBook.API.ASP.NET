using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBook.API.Model
{
    public class Result
    {
        [Key] 
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Semester))]
        public int SemesterId { get; set; }
        [ForeignKey(nameof(Grade))]
        public int GradeId { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
    }

    public class SecondaryResult
    {
        public int Id { get; set; }
        public int Sl { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        [ForeignKey(nameof(Grade))]
        public int GradeId { get; set; }
        public int IsOptional { get; set; }
        public int Level { get; set; }
    }

    public class SecondaryResultTable : SecondaryResult
    {
        public string GradeName { get; set; }
        public decimal Points { get; set; }
    }

    public class SecondarySummary
    {
        public int Level { get; set; }
        public int TotalSubjects { get; set; }
        public decimal TotalPoints { get; set; }
    }

    public class AllSecondaryResults{
        public List<SecondaryResultTable>? Results { get; set; } = new List<SecondaryResultTable>() ;
        public List<SecondarySummary> Summary { get; set; } = new List<SecondarySummary> () ;
    }
}
