using MongoDB.Driver;

public interface IUserService
{
    Task<List<Users>> GetAllAsync();
    Task<Users> GetByIdAsync(string id);
    Task<Users> GetByUsernameAsync(string username);
    Task<Users> CreateAsync(CreateUsersModel createUserModel);
    Task UpdateAsync(string id, CreateUsersModel updateUserModel);
    Task DeleteAsync(string id);
}

//public class UserService : IUserService
//{
//    private readonly IUserRepository _userRepository;

//    public UserService(IUserRepository userRepository)
//    {
//        _userRepository = userRepository;
//    }

//    public async Task<List<Users>> GetAllAsync()
//    {
//        return await _userRepository.GetAllAsync();
//    }

//    public async Task<Users> GetByIdAsync(string id)
//    {
//        return await _userRepository.GetByIdAsync(id);
//    }

//    public async Task<Users> GetByUsernameAsync(string username)
//    {
//        return await _userRepository.GetByUsernameAsync(username);
//    }

//    public async Task<Users> CreateAsync(CreateUsersModel createUserModel)
//    {
//        var user = new Users
//        {
//            Username = createUserModel.Username,
//            FirstName = createUserModel.FirstName,
//            LastName = createUserModel.LastName,
//            Email = createUserModel.Email,
//            Password = createUserModel.Password, // Здесь должен быть хеш пароля
//            Phone = createUserModel.Phone,
//            Role = createUserModel.Role,
//            Status = createUserModel.Status
//        };
//        await _userRepository.CreateAsync(user);
//        return user;
//    }

//    public async Task UpdateAsync(string id, CreateUsersModel updateUserModel)
//    {
//        var update = Builders<Users>.Update
//            .Set(u => u.Username, updateUserModel.Username)
//            .Set(u => u.FirstName, updateUserModel.FirstName)
//            .Set(u => u.LastName, updateUserModel.LastName)
//            .Set(u => u.Email, updateUserModel.Email)
//            .Set(u => u.Password, updateUserModel.Password) // Здесь должен быть хеш пароля
//            .Set(u => u.Phone, updateUserModel.Phone)
//            .Set(u => u.Role, updateUserModel.Role)
//            .Set(u => u.Status, updateUserModel.Status);
//        await _userRepository.UpdateAsync(id, update);
//    }

//    public async Task DeleteAsync(string id)
//    {
//        await _userRepository.DeleteAsync(id);
//    }
//}


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Users>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<Users> GetByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<Users> GetByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }

    public async Task<Users> CreateAsync(CreateUsersModel createUserModel)
    {
        var user = new Users
        {
            Username = createUserModel.Username,
            FirstName = createUserModel.FirstName,
            LastName = createUserModel.LastName,
            Email = createUserModel.Email,
            Password = createUserModel.Password, // Здесь должен быть хеш пароля
            Phone = createUserModel.Phone,
            Role = createUserModel.Role,
            Status = "Active" // Устанавливаем значение по умолчанию
        };
        await _userRepository.CreateAsync(user);
        return user;
    }

    public async Task UpdateAsync(string id, CreateUsersModel updateUserModel)
    {
        var update = Builders<Users>.Update
            .Set(u => u.Username, updateUserModel.Username)
            .Set(u => u.FirstName, updateUserModel.FirstName)
            .Set(u => u.LastName, updateUserModel.LastName)
            .Set(u => u.Email, updateUserModel.Email)
            .Set(u => u.Password, updateUserModel.Password) // Здесь должен быть хеш пароля
            .Set(u => u.Phone, updateUserModel.Phone)
            .Set(u => u.Role, updateUserModel.Role);
        await _userRepository.UpdateAsync(id, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
