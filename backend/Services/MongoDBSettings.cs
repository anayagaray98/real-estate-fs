namespace RealEstateAPI.Services
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string OwnersCollectionName { get; set; } = "owners";
        public string PropertiesCollectionName { get; set; } = "properties";
        public string PropertyImagesCollectionName { get; set; } = "propertyImages";
        public string PropertyTracesCollectionName { get; set; } = "propertyTraces";
    }
}
