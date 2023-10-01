using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBook.API.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }
        [MaxLength(10)]
        public string? CourseCode { get; set; }
        [MaxLength(100)]
        public string? CourseTitle { get; set;}
        public decimal CreditPoint { get; set; }
    }
}
