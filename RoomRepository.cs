using MongoDB.Driver;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room> GetByIdAsync(string id);
    Task CreateAsync(Room room);
    Task UpdateAsync(string id, UpdateDefinition<Room> update);
    Task DeleteAsync(string id);
    Task<List<Room>> GetRoomsByGardenIdAsync(string gardenId);
}

//public class RoomRepository : IRoomRepository
//{
//    private readonly IMongoCollection<Room> _collection;

//    public RoomRepository(IMongoDatabase database)
//    {
//        _collection = database.GetCollection<Room>("Rooms");
//    }

//    public async Task<List<Room>> GetAllAsync()
//    {
//        return await _collection.Find(_ => true).ToListAsync();
//    }

//    public async Task<Room> GetByIdAsync(string id)
//    {
//        return await _collection.Find(Builders<Room>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
//    }

//    public async Task CreateAsync(Room room)
//    {
//        await _collection.InsertOneAsync(room);
//    }

//    public async Task UpdateAsync(string id, UpdateDefinition<Room> update)
//    {
//        await _collection.UpdateOneAsync(Builders<Room>.Filter.Eq("Id", id), update);
//    }

//    public async Task DeleteAsync(string id)
//    {
//        await _collection.DeleteOneAsync(Builders<Room>.Filter.Eq("Id", id));
//    }
//}

public class RoomRepository : IRoomRepository
{
    private readonly IMongoCollection<Room> _collection;

    public RoomRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Room>("Rooms");
    }

    public async Task<List<Room>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Room> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<Room>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task<List<Room>> GetRoomsByGardenIdAsync(string gardenId)
    {
        var filter = Builders<Room>.Filter.Eq(r => r.GardenId, gardenId);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task CreateAsync(Room room)
    {
        await _collection.InsertOneAsync(room);
    }

    public async Task UpdateAsync(string id, UpdateDefinition<Room> update)
    {
        await _collection.UpdateOneAsync(Builders<Room>.Filter.Eq("Id", id), update);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<Room>.Filter.Eq("Id", id));
    }
}

