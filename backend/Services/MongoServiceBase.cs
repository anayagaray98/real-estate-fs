using MongoDB.Driver;

namespace RealEstateAPI.Services
{
    public abstract class MongoServiceBase<T>
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;

        protected MongoServiceBase(IMongoDatabase database, string collectionName)
        {
            _database = database;
            _collection = database.GetCollection<T>(collectionName);
        }

        public virtual async Task<List<T>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public virtual async Task<T?> GetByIdAsync(string id) =>
            await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

        public virtual async Task CreateAsync(T item) =>
            await _collection.InsertOneAsync(item);

        public virtual async Task UpdateAsync(string id, T item) =>
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), item);

        public virtual async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }
}
