using MongoDB.Driver;
using RealEstateAPI.Models;
using RealEstateAPI.Dtos;

namespace RealEstateAPI.Services
{
    public class OwnerService : MongoServiceBase<Owner>
    {
        public OwnerService(IMongoDatabase database)
            : base(database, "owners")
        {
        }

        // Get all owners
        public async Task<List<Owner>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Create a new owner
        public async Task<Owner> CreateAsync(OwnerDto dto)
        {
            var owner = new Owner
            {
                Name = dto.Name,
                Address = dto.Address,
                Photo = dto.Photo,
                Birthday = dto.Birthday
            };

            await base.CreateAsync(owner);
            return owner;
        }

        // Get owner by ID
        public async Task<Owner?> GetByIdAsync(string id)
        {
            var owner = await _collection.Find(o => o.IdOwner == id).FirstOrDefaultAsync();
            if (owner == null)
                return null;

            return new Owner
            {
                IdOwner = owner.IdOwner,
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo,
                Birthday = owner.Birthday
            };
        }

        // Update an existing owner
        public async Task<Owner?> UpdateAsync(string id, OwnerDto updatedOwner)
        {
            var owner = new Owner
            {
                IdOwner = id,
                Name = updatedOwner.Name,
                Address = updatedOwner.Address,
                Photo = updatedOwner.Photo,
                Birthday = updatedOwner.Birthday
            };

            var result = await _collection.ReplaceOneAsync(o => o.IdOwner == id, owner);
            if (result.MatchedCount == 0)
                return null; // no owner found

            return owner;
        }

        // Delete an owner
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(o => o.IdOwner == id);
            return result.DeletedCount > 0;
        }
    }
}
