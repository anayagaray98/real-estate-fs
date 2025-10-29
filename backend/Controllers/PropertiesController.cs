using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Services;
using RealEstateAPI.Dtos;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        public PropertiesController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // GET: api/properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyListDto>>> GetProperties(
            [FromQuery] string? name,
            [FromQuery] string? address,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var propertiesWithImage = await _propertyService.GetAllWithOneImageAsync(name, address, minPrice, maxPrice);

            var dtoList = propertiesWithImage.Select(tuple => new PropertyListDto
            {
                IdProperty = tuple.Property.IdProperty,
                Name = tuple.Property.Name,
                Address = tuple.Property.Address,
                Price = tuple.Property.Price,
                Year = tuple.Property.Year,
                CodeInternal = tuple.Property.CodeInternal,
                IdOwner = tuple.Property.IdOwner,
                Image = tuple.Image
            });

            return Ok(dtoList);
        }

        // GET: api/properties/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDetailDto>> GetById(string id)
        {
            var (property, images, traces) = await _propertyService.GetByIdWithDetailsAsync(id);

            if (property == null)
                return NotFound();

            var dto = new PropertyDetailDto
            {
                IdProperty = property.IdProperty,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner,
                Images = images.Select(img => new PropertyImageDto
                {
                    IdPropertyImage = img.IdPropertyImage,
                    File = img.File,
                    Enabled = img.Enabled
                }).ToList(),
                Traces = traces.Select(trace => new PropertyTraceDto
                {
                    IdPropertyTrace = trace.IdPropertyTrace,
                    DateSale = trace.DateSale,
                    Name = trace.Name,
                    Value = trace.Value,
                    Tax = trace.Tax
                }).ToList()
            };

            return Ok(dto);
        }

        // POST: api/properties
        [HttpPost]
        public async Task<ActionResult<PropertyDetailDto>> Create([FromBody] PropertyCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _propertyService.CreateAsync(dto);

            var result = new PropertyDetailDto
            {
                IdProperty = created.IdProperty,
                Name = created.Name,
                Address = created.Address,
                Price = created.Price,
                CodeInternal = created.CodeInternal,
                Year = created.Year,
                IdOwner = created.IdOwner,
                Images = dto.Images?.Select(img => new PropertyImageDto
                {
                    File = img,
                    Enabled = true
                }).ToList() ?? new List<PropertyImageDto>(),
                Traces = new List<PropertyTraceDto>()
            };

            return CreatedAtAction(nameof(GetById), new { id = created.IdProperty }, result);
        }
        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDelete([FromBody] PropertyBulkDeleteDto request)
        {
            if (request.Ids == null || !request.Ids.Any())
                return BadRequest(new { message = "No property IDs provided." });

            var deletedCount = await _propertyService.DeleteManyAsync(request.Ids);

            return Ok(new
            {
                message = $"Successfully deleted {deletedCount} properties.",
                deletedCount
            });
        }
    }
}
