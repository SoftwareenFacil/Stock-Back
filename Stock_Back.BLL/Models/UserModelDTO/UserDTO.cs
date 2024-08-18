namespace Stock_Back.BLL.Models.UserModelDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public bool SuperAdmin { get; set; }
        public string? Job { get; set; }
    }
}
