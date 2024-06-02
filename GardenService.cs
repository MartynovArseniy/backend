//using Lb2.Repositories;
using MongoDB.Driver;

public interface IGardenService
{
    Task<List<Garden>> GetAllAsync();
    Task<Garden> GetByIdAsync(string id);
    Task CreateAsync(GardenCreateModel gardenCreateModel);
    Task UpdateAsync(string id, GardenUpdateModel updateModel);
    Task DeleteAsync(string id);
}
//public interface IGardenService
//{
//    Task<List<Garden>> GetAllAsync();
//    Task<Garden> GetByIdAsync(string id);
//    Task<List<User>?> GetUsersByGardenAsync(string gardenId);
//    Task CreateAsync(GardenCreateModel gardenCreateModel);
//    Task UpdateAsync(string id, GardenUpdateModel updateModel);
//    Task DeleteAsync(string id);
//}
public class GardenService : IGardenService
{
    private readonly IGardenRepository _gardenRepository;

    public GardenService(IGardenRepository gardenRepository)
    {
        _gardenRepository = gardenRepository;
    }

    public async Task<List<Garden>> GetAllAsync()
    {
        return await _gardenRepository.GetAllAsync();
    }

    public async Task<Garden> GetByIdAsync(string id)
    {
        return await _gardenRepository.GetByIdAsync(id);
    }

    public async Task CreateAsync(GardenCreateModel gardenCreateModel)
    {
        var garden = new Garden
        {
            Name = gardenCreateModel.Name,
            Location = gardenCreateModel.Location,
            Director = gardenCreateModel.Director,
            Email = gardenCreateModel.Email,
            Phone = gardenCreateModel.Phone,
            Users = gardenCreateModel.Users
        };
        await _gardenRepository.CreateAsync(garden);
    }

    //public async Task UpdateAsync(string id, GardenUpdateModel updateModel)
    //{
    //    var update = Builders<Garden>.Update
    //        .Set(g => g.Name, updateModel.Name)
    //        .Set(g => g.Location, updateModel.Location)
    //        .Set(g => g.Director, updateModel.Director)
    //        .Set(g => g.Email, updateModel.Email)
    //        .Set(g => g.Phone, updateModel.Phone)
    //        .Set(g => g.Users, updateModel.Users ?? new List<UserReference>());
    //    await _gardenRepository.UpdateAsync(id, update);
    //}
    //public async Task UpdateAsync(string id, GardenUpdateModel updateModel)
    //{
    //    var update = Builders<Garden>.Update
    //        .Set(g => g.Name, updateModel.Name)
    //        .Set(g => g.Location, updateModel.Location)
    //        .Set(g => g.Director, updateModel.Director)
    //        .Set(g => g.Email, updateModel.Email)
    //        .Set(g => g.Phone, updateModel.Phone)
    //        .Set(g => g.Users, updateModel.Users.Select(u => new UserReference { Id = u.Id }).ToList());

    //    await _gardenRepository.UpdateAsync(id, update);
    //}
    public async Task UpdateAsync(string id, GardenUpdateModel updateModel)
    {
        var garden = await _gardenRepository.GetByIdAsync(id);
        if (garden == null)
        {
            throw new Exception("Garden not found");
        }

        // Добавляем новых пользователей, не удаляя старых
        var updatedUsers = garden.Users.Concat(updateModel.Users.Where(u => !garden.Users.Any(existingUser => existingUser.Id == u.Id))).ToList();

        var update = Builders<Garden>.Update
            .Set(g => g.Name, updateModel.Name)
            .Set(g => g.Location, updateModel.Location)
            .Set(g => g.Director, updateModel.Director)
            .Set(g => g.Email, updateModel.Email)
            .Set(g => g.Phone, updateModel.Phone)
            .Set(g => g.Users, updatedUsers);

        await _gardenRepository.UpdateAsync(id, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _gardenRepository.DeleteAsync(id);
    }
}

//public class GardenService : IGardenService
//{
//    private readonly IGardenRepository _gardenRepository;

//    public GardenService(IGardenRepository gardenRepository)
//    {
//        _gardenRepository = gardenRepository;
//    }

//    public async Task<List<Garden>> GetAllAsync()
//    {
//        return await _gardenRepository.GetAllAsync();
//    }

//    public async Task<Garden> GetByIdAsync(string id)
//    {
//        return await _gardenRepository.GetByIdAsync(id);
//    }

//    public async Task CreateAsync(GardenCreateModel gardenCreateModel)
//    {
//        var garden = new Garden
//        {
//            Name = gardenCreateModel.Name,
//            Location = gardenCreateModel.Location,
//            Director = gardenCreateModel.Director,
//            Email = gardenCreateModel.Email,
//            Phone = gardenCreateModel.Phone,
//            Users = gardenCreateModel.Users
//        };
//        await _gardenRepository.CreateAsync(garden);
//    }

//    public async Task UpdateAsync(string id, GardenUpdateModel updateModel)
//    {
//        var update = Builders<Garden>.Update
//            .Set(g => g.Name, updateModel.Name)
//            .Set(g => g.Location, updateModel.Location)
//            .Set(g => g.Director, updateModel.Director)
//            .Set(g => g.Email, updateModel.Email)
//            .Set(g => g.Phone, updateModel.Phone)
//            .Set(g => g.Users, updateModel.Users ?? new List<UserReference>());
//        await _gardenRepository.UpdateAsync(id, update);
//    }

//    public async Task DeleteAsync(string id)
//    {
//        await _gardenRepository.DeleteAsync(id);
//    }
//}

//public class GardenService : IGardenService
//{
//    private readonly IGardenRepository _gardenRepository;

//    public GardenService(IGardenRepository gardenRepository)
//    {
//        _gardenRepository = gardenRepository;
//    }

//    public async Task<List<Garden>> GetAllAsync()
//    {
//        return await _gardenRepository.GetAllAsync();
//    }

//    public async Task<Garden> GetByIdAsync(string id)
//    {
//        return await _gardenRepository.GetByIdAsync(id);
//    }

//    public async Task<List<User>?> GetUsersByGardenAsync(string gardenId)
//    {
//        var garden = await _gardenRepository.GetByIdAsync(gardenId);
//        if (garden == null || garden.Users == null)
//        {
//            return null;
//        }

//        List<User> users = new List<User>();
//        foreach (var userRef in garden.Users)
//        {
//            // Предположим, что есть метод GetByIdAsync для получения объекта User по Id
//            var user = await _userRepository.GetByIdAsync(userRef.Id);
//            if (user != null)
//            {
//                users.Add(user);
//            }
//        }

//        return users;
//    }


//    public async Task CreateAsync(GardenCreateModel gardenCreateModel)
//    {
//        var garden = new Garden
//        {
//            Name = gardenCreateModel.Name,
//            Location = gardenCreateModel.Location,
//            Director = gardenCreateModel.Director,
//            Email = gardenCreateModel.Email,
//            Phone = gardenCreateModel.Phone,
//            Users = gardenCreateModel.Users
//        };
//        await _gardenRepository.CreateAsync(garden);
//    }

//    public async Task UpdateAsync(string id, GardenUpdateModel updateModel)
//    {
//        var garden = await _gardenRepository.GetByIdAsync(id);
//        if (garden == null)
//        {
//            throw new Exception("Садок з таким ID не знайдено.");
//        }

//        var update = Builders<Garden>.Update
//            .Set(g => g.Name, updateModel.Name)
//            .Set(g => g.Location, updateModel.Location)
//            .Set(g => g.Director, updateModel.Director)
//            .Set(g => g.Email, updateModel.Email)
//            .Set(g => g.Phone, updateModel.Phone)
//            .Set(g => g.Users, updateModel.Users);

//        await _gardenRepository.UpdateAsync(id, update);
//    }


//    public async Task DeleteAsync(string id)
//    {
//        await _gardenRepository.DeleteAsync(id);
//    }
//}
