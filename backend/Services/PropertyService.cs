using MongoDB.Driver;
using RealEstateAPI.Models;
using Microsoft.Extensions.Options;

namespace RealEstateAPI.Services
{
    public class PropertyService
    {
        private readonly IMongoCollection<Property> _propertiesCollection;

        public PropertyService(IOptions<MongoDBSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _propertiesCollection = database.GetCollection<Property>(mongoSettings.Value.PropertiesCollectionName);
        }

        public async Task<List<Property>> GetAllAsync(
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

            var finalFilter = filters.Any() ? filterBuilder.And(filters) : FilterDefinition<Property>.Empty;

            return await _propertiesCollection.Find(finalFilter).ToListAsync();
        }
    }

    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PropertiesCollectionName { get; set; } = null!;
    }
}
