namespace Stock_Back.BLL.Models.UserModelDTO
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Password { get; set; } = string.Empty;
        public string? Job { get; set; }
        public bool SuperAdmin { get; set; }
    }
}
