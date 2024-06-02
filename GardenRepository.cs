using MongoDB.Driver;

public interface IGardenRepository
{
    Task<List<Garden>> GetAllAsync();
    Task<Garden> GetByIdAsync(string id);
    Task CreateAsync(Garden garden);
    Task UpdateAsync(string id, UpdateDefinition<Garden> update);
    Task DeleteAsync(string id);
}
public class GardenRepository : IGardenRepository
{
    private readonly IMongoCollection<Garden> _collection;

    public GardenRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Garden>("Gardens");
    }

    public async Task<List<Garden>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Garden> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<Garden>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Garden garden)
    {
        await _collection.InsertOneAsync(garden);
    }

    public async Task UpdateAsync(string id, UpdateDefinition<Garden> update)
    {
        await _collection.UpdateOneAsync(Builders<Garden>.Filter.Eq("Id", id), update);
    }
    //public async Task UpdateAsync(string id, UpdateDefinition<Garden> update)
    //{
    //    await _collection.UpdateOneAsync(Builders<Garden>.Filter.Eq("Id", id), update);
    //}


    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<Garden>.Filter.Eq("Id", id));
    }
}
