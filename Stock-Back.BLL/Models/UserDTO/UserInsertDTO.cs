﻿namespace Stock_Back.BLL.Models.UserDTO
{
    public class UserInsertDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
