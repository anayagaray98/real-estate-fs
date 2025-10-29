namespace RealEstateAPI.Dtos
{
    public class OwnerDto
    {
        public string? IdOwner { get; set; } 
        public string Name { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Photo { get; set; } = null!;
        public DateTime? Birthday { get; set; }
    }
}