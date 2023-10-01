using System.ComponentModel.DataAnnotations;

namespace PersonalBook.API.Model
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string GradeName { get; set; }
        public decimal Points { get; set; }
        public int Scale { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
    }
}
