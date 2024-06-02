// Repositories/IAdminUserRepository.cs
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Collections.Generic;

public interface IAdminUserRepository
{
    Task<List<AdminUser>> GetAllAsync();
    Task<AdminUser> GetByIdAsync(string id);
    Task<AdminUser> GetByUsernameAsync(string username);
    Task CreateAsync(AdminUser adminUser);
    Task UpdateAsync(string id, UpdateDefinition<AdminUser> update);
    Task DeleteAsync(string id);
}

// Repositories/AdminUserRepository.cs
public class AdminUserRepository : IAdminUserRepository
{
    private readonly IMongoCollection<AdminUser> _collection;

    public AdminUserRepository(IMongoClient client)
    {
        var database = client.GetDatabase("ChildClimaCare");
        _collection = database.GetCollection<AdminUser>("User");
    }

    public async Task<List<AdminUser>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<AdminUser> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<AdminUser>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }
    public async Task<AdminUser> GetByUsernameAsync(string username)
    {
        var filter = Builders<AdminUser>.Filter.Eq(u => u.Username, username);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task CreateAsync(AdminUser adminUser)
    {
        await _collection.InsertOneAsync(adminUser);
    }

    public async Task UpdateAsync(string id, UpdateDefinition<AdminUser> update)
    {
        await _collection.UpdateOneAsync(Builders<AdminUser>.Filter.Eq("Id", id), update);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<AdminUser>.Filter.Eq("Id", id));
    }
}

// Аналогичные репозитории для Garden, Equipment, Measurement и User
