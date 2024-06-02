using MongoDB.Driver;

public interface IEquipmentRepository
{
    Task<List<Equipment>> GetAllAsync();
    Task<Equipment> GetByIdAsync(string id);
    Task CreateAsync(Equipment equipment);
    Task UpdateAsync(string id, UpdateDefinition<Equipment> update);
    Task DeleteAsync(string id);
    Task<List<Equipment>> GetByRoomIdAsync(string roomId);
}

public class EquipmentRepository : IEquipmentRepository
{
    private readonly IMongoCollection<Equipment> _collection;

    public EquipmentRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Equipment>("Equipment");
    }

    public async Task<List<Equipment>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Equipment> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<Equipment>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task<List<Equipment>> GetByRoomIdAsync(string roomId)
    {
        return await _collection.Find(Builders<Equipment>.Filter.Eq(e => e.RoomId, roomId)).ToListAsync();
    }

    public async Task CreateAsync(Equipment equipment)
    {
        await _collection.InsertOneAsync(equipment);
    }

    public async Task UpdateAsync(string id, UpdateDefinition<Equipment> update)
    {
        await _collection.UpdateOneAsync(Builders<Equipment>.Filter.Eq("Id", id), update);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<Equipment>.Filter.Eq("Id", id));
    }
}
