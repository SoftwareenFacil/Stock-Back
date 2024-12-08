using System.ComponentModel.DataAnnotations;

namespace EIC_Back.BLL.Models.UserModelDTO
{
    public class UserInsertDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public bool SuperAdmin { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string Job { get; set; }
    }
}
