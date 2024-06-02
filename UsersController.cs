using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


//[Route("api/[controller]")]
//[ApiController]
//public class UsersController : ControllerBase
//{
//    private readonly IMongoCollection<Users> _usersCollection;

//    public UsersController(IMongoClient client)
//    {
//        var database = client.GetDatabase("ChildClimaCare");
//        _usersCollection = database.GetCollection<Users>("User");
//    }



//    [HttpGet("{id:length(24)}", Name = "GetUserById")]
//    public async Task<ActionResult<Users>> GetUserById(string id)
//    {
//        var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
//        if (user == null)
//        {
//            return NotFound("Користувача з таким ID не знайдено.");
//        }
//        return user;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] CreateUsersModel createUserModel)
//    {
//        // Перевірка, чи існує користувач з таким Email або Username
//        var existingUser = await _usersCollection.Find(u => u.Email == createUserModel.Email || u.Username == createUserModel.Username).FirstOrDefaultAsync();
//        if (existingUser != null)
//        {
//            return BadRequest("Користувач з таким Email або Username вже існує.");
//        }

//        var user = new Users
//        {
//            Username = createUserModel.Username,
//            FirstName = createUserModel.FirstName,
//            LastName = createUserModel.LastName,
//            Email = createUserModel.Email,
//            Password = createUserModel.Password, // Тут має бути хешування паролю перед збереженням
//            Phone = createUserModel.Phone,
//            Role = createUserModel.Role,
//            Status = createUserModel.Status
//        };

//        await _usersCollection.InsertOneAsync(user);
//        return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
//    {
//        var user = await _usersCollection.Find(u => u.Username == loginModel.Username).FirstOrDefaultAsync();

//        if (user == null || user.Password != loginModel.Password)
//        {
//            return Unauthorized("Неправильне ім'я користувача або пароль.");
//        }

//        if (user.Status == "Banned")
//        {
//            return Unauthorized("Ваш обліковий запис заблоковано.");
//        }


//        return Ok("Вхід в систему успішний.");
//    }



//    // Додайте тут інші методи API...
//}
//[Route("api/[controller]")]
//[ApiController]
//public class UsersController : ControllerBase
//{
//    private readonly IUserService _userService;

//    public UsersController(IUserService userService)
//    {
//        _userService = userService;
//    }

//    [HttpGet]
//    public async Task<ActionResult<List<Users>>> GetAllUsers()
//    {
//        var users = await _userService.GetAllAsync();
//        return Ok(users);
//    }

//    [HttpGet("{id:length(24)}", Name = "GetUserById")]
//    public async Task<ActionResult<Users>> GetUserById(string id)
//    {
//        var user = await _userService.GetByIdAsync(id);
//        if (user == null)
//        {
//            return NotFound("User with this ID not found.");
//        }
//        return Ok(user);
//    }

//    [HttpGet("username/{username}", Name = "GetUserByUsername")]
//    public async Task<ActionResult<Users>> GetUserByUsername(string username)
//    {
//        var user = await _userService.GetByUsernameAsync(username);
//        if (user == null)
//        {
//            return NotFound("User with this username not found.");
//        }
//        return Ok(user);
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] CreateUsersModel createUserModel)
//    {
//        var createdUser = await _userService.CreateAsync(createUserModel);
//        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
//    }

//    [HttpPut("{id:length(24)}")]
//    public async Task<IActionResult> UpdateUser(string id, [FromBody] CreateUsersModel updateUserModel)
//    {
//        await _userService.UpdateAsync(id, updateUserModel);
//        return Ok("User updated successfully.");
//    }

//    [HttpDelete("{id:length(24)}")]
//    public async Task<IActionResult> DeleteUser(string id)
//    {
//        await _userService.DeleteAsync(id);
//        return Ok("User deleted successfully.");
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
//    {
//        var user = await _userService.GetByUsernameAsync(loginModel.Username);

//        if (user == null || user.Password != loginModel.Password) // Здесь должна быть проверка хеша пароля
//        {
//            return Unauthorized("Invalid username or password.");
//        }

//        if (user.Status == "Banned")
//        {
//            return Unauthorized("Your account has been banned.");
//        }

//        return Ok(new { message = "Login successful" });
//    }
//}

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Users>>> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:length(24)}", Name = "GetUserById")]
    public async Task<ActionResult<Users>> GetUserById(string id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("User with this ID not found.");
        }
        return Ok(user);
    }

    [HttpGet("username/{username}", Name = "GetUserByUsername")]
    public async Task<ActionResult<Users>> GetUserByUsername(string username)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user == null)
        {
            return NotFound("User with this username not found.");
        }
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUsersModel createUserModel)
    {
        var createdUser = await _userService.CreateAsync(createUserModel);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] CreateUsersModel updateUserModel)
    {
        await _userService.UpdateAsync(id, updateUserModel);
        return Ok("User updated successfully.");
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteAsync(id);
        return Ok("User deleted successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var user = await _userService.GetByUsernameAsync(loginModel.Username);

        if (user == null || user.Password != loginModel.Password) // Здесь должна быть проверка хеша пароля
        {
            return Unauthorized("Invalid username or password.");
        }

        if (user.Status == "Banned")
        {
            return Unauthorized("Your account has been banned.");
        }

        return Ok(new { message = "Login successful" });
    }
}

[BsonIgnoreExtraElements]
public class Users
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // Пароль не повинен бути відображений у відповіді API
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
}


public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class CreateUsersModel
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
}
