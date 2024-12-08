namespace EIC_Back.BLL.Models.ClientModelDTO
{
    public class FullClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; } = string.Empty;

        public bool Vigency { get; set; } = true;

        public DateTime Created { get; set; }
    }
}
