using MongoDB.Driver;
using RealEstateAPI.Models;
using RealEstateAPI.Dtos;

namespace RealEstateAPI.Services
{
    public class PropertyService : MongoServiceBase<Property>
    {
        private readonly IMongoCollection<PropertyImage> _propertyImages;

        public PropertyService(IMongoDatabase database)
            : base(database, "properties")
        {
            // Images collection
            _propertyImages = _database.GetCollection<PropertyImage>("propertyImages");
        }

        // List properties with filters and one image
        public async Task<List<(Property Property, string? Image)>> GetAllWithOneImageAsync(
            string? name = null,
            string? address = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrEmpty(name))
                filters.Add(filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));

            if (!string.IsNullOrEmpty(address))
                filters.Add(filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));

            if (minPrice.HasValue)
                filters.Add(filterBuilder.Gte(p => p.Price, minPrice.Value));

            if (maxPrice.HasValue)
                filters.Add(filterBuilder.Lte(p => p.Price, maxPrice.Value));

            var finalFilter = filters.Any()
                ? filterBuilder.And(filters)
                : FilterDefinition<Property>.Empty;

            // Get properties
            var properties = await _collection.Find(finalFilter).ToListAsync();
            if (!properties.Any())
                return new List<(Property, string?)>();

            // Get images for these properties
            var propertyIds = properties.Select(p => p.IdProperty).ToList();
            var images = await _propertyImages
                .Find(i => propertyIds.Contains(i.IdProperty))
                .ToListAsync();

            // Group by property and take the first image
            var imagesByProperty = images
                .GroupBy(i => i.IdProperty)
                .ToDictionary(g => g.Key, g => g.FirstOrDefault()?.File);

            // Combine results
            var result = properties
                .Select(p => (p, imagesByProperty.GetValueOrDefault(p.IdProperty)))
                .ToList();

            return result;
        }

        // Get property details
        public async Task<(Property? Property, List<PropertyImage> Images, List<PropertyTrace> Traces)> GetByIdWithDetailsAsync(string id)
        {
            // Fetch property
            var property = await _collection.Find(p => p.IdProperty == id).FirstOrDefaultAsync();
            if (property == null)
                return (null, new List<PropertyImage>(), new List<PropertyTrace>());

            // Fetch related images
            var images = await _propertyImages
                .Find(img => img.IdProperty == id)
                .ToListAsync();

            // Fetch related traces
            var traces = await _database
                .GetCollection<PropertyTrace>("propertyTraces")
                .Find(trace => trace.IdProperty == id)
                .ToListAsync();

            return (property, images, traces);
        }

        // Create property and its associated images
        public async Task<Property> CreateAsync(PropertyCreateDto dto)
        {
            var newProperty = new Property
            {
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                CodeInternal = dto.CodeInternal,
                Year = dto.Year,
                IdOwner = dto.IdOwner
            };

            // Save property
            await _collection.InsertOneAsync(newProperty);

            // If property includes images, we create it
            if (dto.Images != null && dto.Images.Any())
            {
                var images = dto.Images.Select(img => new PropertyImage
                {
                    IdProperty = newProperty.IdProperty,
                    File = img,
                    Enabled = true // default
                }).ToList();

                await _propertyImages.InsertManyAsync(images);
            }

            return newProperty;
        }

        // Bulk delete
        public async Task<long> DeleteManyAsync(IEnumerable<string> ids)
        {
            var filter = Builders<Property>.Filter.In(p => p.IdProperty, ids);
            var result = await _collection.DeleteManyAsync(filter);
            
            // Delete related images
            await _database.GetCollection<PropertyImage>("propertyImages")
                .DeleteManyAsync(Builders<PropertyImage>.Filter.In(i => i.IdProperty, ids));

            // Delete related traces
            await _database.GetCollection<PropertyTrace>("propertyTraces")
                .DeleteManyAsync(Builders<PropertyTrace>.Filter.In(t => t.IdProperty, ids));

            return result.DeletedCount;
        }
    }
}
