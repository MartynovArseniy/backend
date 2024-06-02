using MongoDB.Driver;

public interface IUserRepository
{
    Task<List<Users>> GetAllAsync();
    Task<Users> GetByIdAsync(string id);
    Task<Users> GetByUsernameAsync(string username);
    Task CreateAsync(Users user);
    Task UpdateAsync(string id, UpdateDefinition<Users> update);
    Task DeleteAsync(string id);
}

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<Users> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Users>("User");
    }

    public async Task<List<Users>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Users> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<Users>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task<Users> GetByUsernameAsync(string username)
    {
        return await _collection.Find(Builders<Users>.Filter.Eq("Username", username)).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Users user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task UpdateAsync(string id, UpdateDefinition<Users> update)
    {
        await _collection.UpdateOneAsync(Builders<Users>.Filter.Eq("Id", id), update);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<Users>.Filter.Eq("Id", id));
    }
}
