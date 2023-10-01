using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBook.API.Model
{
    public class ApplicationUser : UserBase
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public string? Role { get; set; }
        public string? Token { get; set; }
        public DateTime Created { get; set; }
    }

    public class UserBase
    {
        [MaxLength(20)]
        public string? FirstName { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
        [Key]
        [MaxLength(30)]
        [Column(Order = 1)]
        public string? Email { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(25)]
        public string? Username { get; set; }
        [Key]
        [Column(Order = 3)]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
    }

    public class Login
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
