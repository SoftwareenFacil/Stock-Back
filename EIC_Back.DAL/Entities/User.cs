using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EIC_Back.DAL.Models
{
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string? Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int Tax { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool SuperAdmin { get; set; }
        public bool Vigency { get; set; }
        public string? Job { get; set; }

        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenDate { get; set; }
    }
}
