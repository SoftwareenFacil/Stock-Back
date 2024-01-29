namespace Stock_Back.BLL.Models.UserDTO
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
