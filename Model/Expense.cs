using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBook.API.Model
{
    public enum Category
    {
        Hosing = 1,
        Transportation = 2,
        Food = 3,
        Utilities = 4,
        Insurance = 5,
        MedicalHealth = 6,
        Clothing = 7,
        Household = 8,
        Personal = 9,
        Education = 10,
        Entertainment = 11,
        Others = 12,
    }

    public class Expense
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid UserId { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get ; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }

    public class CategorySummaryRequest
    {
        public Guid UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate {get; set; }
}
}
