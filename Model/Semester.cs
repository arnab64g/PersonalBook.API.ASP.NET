using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBook.API.Model
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }
        [MaxLength(50)]
        public string? SemesterName { get; set; }
        public int MonthBng { get; set; }
        public int Year { get; set; }   
    }
}
