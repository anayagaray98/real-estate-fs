namespace RealEstateAPI.Dtos
{
    public class PropertyListDto
    {
        public string? IdProperty { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string? CodeInternal { get; set; }
        public string IdOwner { get; set; } = null!;
        public string? Image { get; set; }
    }
    public class PropertyDetailDto
    {
        public string? IdProperty { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = null!;
        public int Year { get; set; }
        public string IdOwner { get; set; } = null!;
        public List<PropertyImageDto> Images { get; set; } = new();
        public List<PropertyTraceDto> Traces { get; set; } = new();
    }

    public class PropertyImageDto
    {
        public string? IdPropertyImage { get; set; }
        public string File { get; set; } = null!;
        public bool Enabled { get; set; }
    }

    public class PropertyTraceDto
    {
        public string? IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
    public class PropertyCreateDto
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = null!;
        public int Year { get; set; }
        public string IdOwner { get; set; } = null!;
        public List<string> Images { get; set; } = new();
    }
    public class PropertyBulkDeleteDto
    {
        public List<string> Ids { get; set; } = new();
    }

}