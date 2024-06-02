using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

//[Authorize(Roles = "admin")]
//[Route("api/admin")]
//[ApiController]
//public class AdminController : ControllerBase
//{
//    private readonly IMongoCollection<AdminUser> _usersCollection;

//    public AdminController(IMongoClient client)
//    {
//        var database = client.GetDatabase("ChildClimaCare");
//        _usersCollection = database.GetCollection<AdminUser>("User");
//    }

//    [HttpGet("Users")]
//    public async Task<ActionResult<List<AdminUser>>> GetAllUsers()
//    {
//        var users = await _usersCollection.Find(_ => true).ToListAsync();
//        return users;
//    }

//    [HttpDelete("Users/{id:length(24)}")]
//    public async Task<IActionResult> DeleteUser(string id)
//    {
//        var filter = Builders<AdminUser>.Filter.Eq(u => u.Id, id);
//        var result = await _usersCollection.DeleteOneAsync(filter);

//        if (result.DeletedCount == 0)
//        {
//            return NotFound("Користувача з таким ID не знайдено.");
//        }

//        return Ok("Користувача успішно видалено.");
//    }

//    [HttpPut("Users/{id:length(24)}")]
//    public async Task<IActionResult> UpdateUser(string id, [FromBody] AdminUserUpdateModel adminUserUpdateModel)
//    {
//        var filter = Builders<AdminUser>.Filter.Eq(u => u.Id, id);
//        var update = Builders<AdminUser>.Update
//            .Set(u => u.Username, adminUserUpdateModel.Username)
//            .Set(u => u.FirstName, adminUserUpdateModel.FirstName)
//            .Set(u => u.LastName, adminUserUpdateModel.LastName)
//            .Set(u => u.Email, adminUserUpdateModel.Email)
//            .Set(u => u.Phone, adminUserUpdateModel.Phone)
//            .Set(u => u.Role, adminUserUpdateModel.Role)
//            .Set(u => u.Status, adminUserUpdateModel.Status);

//        var result = await _usersCollection.UpdateOneAsync(filter, update);

//        if (result.ModifiedCount == 0)
//        {
//            return NotFound("Користувача з таким ID не знайдено.");
//        }

//        return Ok("Інформація про користувача оновлена успішно.");
//    }

//    // Додайте тут інші методи API...
//}

//public class AdminUser
//{
//    [BsonId]
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string Id { get; set; }

//    public string Username { get; set; }
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public string Email { get; set; }
//    // Пароль не повинен бути відображений у відповіді API
//    public string Password { get; set; }
//    public string Phone { get; set; }
//    public string Role { get; set; }
//    public string Status { get; set; }
//}

//public class AdminUserUpdateModel
//{
//    public string Username { get; set; }
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public string Email { get; set; }
//    public string Phone { get; set; }
//    public string Role { get; set; }
//    public string Status { get; set; }
//}
// Controllers/AdminController.cs
//[Route("api/admin")]
//[ApiController]
//public class AdminController : ControllerBase
//{
//    private readonly AdminUserService _adminUserService;

//    public AdminController(AdminUserService adminUserService)
//    {
//        _adminUserService = adminUserService;
//    }

//    [HttpGet("Users")]
//    public async Task<ActionResult<List<AdminUser>>> GetAllUsers()
//    {
//        var users = await _adminUserService.GetAllUsersAsync();
//        return Ok(users);
//    }

//    [HttpGet("Users/{id:length(24)}")]
//    public async Task<ActionResult<AdminUser>> GetUserById(string id)
//    {
//        var user = await _adminUserService.GetUserByIdAsync(id);
//        if (user == null)
//        {
//            return NotFound("Користувача з таким ID не знайдено.");
//        }
//        return Ok(user);
//    }

//    [HttpPost("Users")]
//    public async Task<IActionResult> CreateUser([FromBody] AdminUser adminUser)
//    {
//        await _adminUserService.CreateUserAsync(adminUser);
//        return CreatedAtAction(nameof(GetUserById), new { id = adminUser.Id }, adminUser);
//    }

//    [HttpPut("Users/{id:length(24)}")]
//    public async Task<IActionResult> UpdateUser(string id, [FromBody] AdminUserUpdateModel adminUserUpdateModel)
//    {
//        await _adminUserService.UpdateUserAsync(id, adminUserUpdateModel);
//        return Ok("Інформація про користувача оновлена успішно.");
//    }

//    [HttpDelete("Users/{id:length(24)}")]
//    public async Task<IActionResult> DeleteUser(string id)
//    {
//        await _adminUserService.DeleteUserAsync(id);
//        return Ok("Користувача успішно видалено.");
//    }
//}

// Analogous controllers can be created for Gardens, Equipment, and Measurements

//[Route("api/admin")]
//[ApiController]
//public class AdminController : ControllerBase
//{
//    private readonly IAdminUserService _adminUserService;
//    private readonly ITokenService _tokenService;

//    public AdminController(IAdminUserService adminUserService, ITokenService tokenService)
//    {
//        _adminUserService = adminUserService;
//        _tokenService = tokenService;
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
//    {
//        var adminUser = await _adminUserService.GetByUsernameAsync(loginModel.Username);

//        if (adminUser == null || adminUser.Password != loginModel.Password) // Здесь должна быть проверка хеша пароля
//        {
//            return Unauthorized("Invalid username or password.");
//        }

//        var token = _tokenService.GenerateToken(adminUser);
//        return Ok(new { token });
//    }

//    [Authorize(Policy = "AdminOnly")]
//    [HttpGet("Users")]
//    public async Task<ActionResult<List<AdminUser>>> GetAllUsers()
//    {
//        var users = await _adminUserService.GetAllUsersAsync();
//        return Ok(users);
//    }

//    [Authorize(Policy = "AdminOnly")]
//    [HttpGet("Users/{id:length(24)}")]
//    public async Task<ActionResult<AdminUser>> GetUserById(string id)
//    {
//        var user = await _adminUserService.GetUserByIdAsync(id);
//        if (user == null)
//        {
//            return NotFound("Користувача з таким ID не знайдено.");
//        }
//        return Ok(user);
//    }

//    [Authorize(Policy = "AdminOnly")]
//    [HttpPost("Users")]
//    public async Task<IActionResult> CreateUser([FromBody] AdminUser adminUser)
//    {
//        await _adminUserService.CreateUserAsync(adminUser);
//        return CreatedAtAction(nameof(GetUserById), new { id = adminUser.Id }, adminUser);
//    }

//    [Authorize(Policy = "AdminOnly")]
//    [HttpPut("Users/{id:length(24)}")]
//    public async Task<IActionResult> UpdateUser(string id, [FromBody] AdminUserUpdateModel adminUserUpdateModel)
//    {
//        await _adminUserService.UpdateUserAsync(id, adminUserUpdateModel);
//        return Ok("Інформація про користувача оновлена успішно.");
//    }

//    [Authorize(Policy = "AdminOnly")]
//    [HttpDelete("Users/{id:length(24)}")]
//    public async Task<IActionResult> DeleteUser(string id)
//    {
//        await _adminUserService.DeleteUserAsync(id);
//        return Ok("Користувача успішно видалено.");
//    }
//}

[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminUserService _adminUserService;
    private readonly ITokenService _tokenService;
    private readonly IGardenService _gardenService;
    private readonly IRoomService _roomService;
    private readonly IEquipmentService _equipmentService;
    private readonly IMeasurementService _measurementService;

    public AdminController(IAdminUserService adminUserService, ITokenService tokenService, IGardenService gardenService, IRoomService roomService,
        IEquipmentService equipmentService,
        IMeasurementService measurementService)
    {
        _adminUserService = adminUserService;
        _tokenService = tokenService;
        _gardenService = gardenService;
        _roomService = roomService;
        _equipmentService = equipmentService;
        _measurementService = measurementService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var adminUser = await _adminUserService.GetByUsernameAsync(loginModel.Username);

        if (adminUser == null || adminUser.Password != loginModel.Password) // Здесь должна быть проверка хеша пароля
        {
            return Unauthorized("Invalid username or password.");
        }

        var token = _tokenService.GenerateToken(adminUser);
        return Ok(new { token, role = adminUser.Role });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Users")]
    public async Task<ActionResult<List<AdminUser>>> GetAllUsers()
    {
        var users = await _adminUserService.GetAllUsersAsync();
        return Ok(users);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Users/{id:length(24)}")]
    public async Task<ActionResult<AdminUser>> GetUserById(string id)
    {
        var user = await _adminUserService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("Користувача з таким ID не знайдено.");
        }
        return Ok(user);
    }

    //[Authorize(Policy = "AdminOnly")]
    //[HttpPost("Users")]
    //public async Task<IActionResult> CreateUser([FromBody] AdminUser adminUser)
    //{
    //    await _adminUserService.CreateUserAsync(adminUser);
    //    return CreatedAtAction(nameof(GetUserById), new { id = adminUser.Id }, adminUser);
    //}
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Users")]
    public async Task<IActionResult> CreateUser([FromBody] AdminUser adminUser)
    {
        if (adminUser.Id != null)
        {
            adminUser.Id = null; // Убедитесь, что Id не передается
        }

        await _adminUserService.CreateUserAsync(adminUser);
        return CreatedAtAction(nameof(GetUserById), new { id = adminUser.Id }, adminUser);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Users/{id:length(24)}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] AdminUserUpdateModel adminUserUpdateModel)
    {
        await _adminUserService.UpdateUserAsync(id, adminUserUpdateModel);
        return Ok("Інформація про користувача оновлена успішно.");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("Users/{id:length(24)}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _adminUserService.DeleteUserAsync(id);
        return Ok("Користувача успішно видалено.");
    }

    // Методы для администрирования садков

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Gardens")]
    public async Task<ActionResult<List<Garden>>> GetAllGardens()
    {
        var gardens = await _gardenService.GetAllAsync();
        return Ok(gardens);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Gardens/{id:length(24)}")]
    public async Task<ActionResult<Garden>> GetGarden(string id)
    {
        var garden = await _gardenService.GetByIdAsync(id);
        if (garden == null)
        {
            return NotFound();
        }
        return Ok(garden);
    }

    //[Authorize(Policy = "AdminOnly")]
    //[HttpPost("Gardens")]
    //public async Task<IActionResult> CreateGarden([FromBody] GardenCreateModel gardenCreateModel)
    //{
    //    await _gardenService.CreateAsync(gardenCreateModel);
    //    return Ok("Садок створено успішно.");
    //}

    //[Authorize(Policy = "AdminOnly")]
    //[HttpPut("Gardens/{id:length(24)}")]
    //public async Task<IActionResult> UpdateGarden(string id, [FromBody] GardenUpdateModel gardenUpdateModel)
    //{
    //    await _gardenService.UpdateAsync(id, gardenUpdateModel);
    //    return Ok("Інформація про садок оновлена успішно.");
    //}

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Gardens")]
    public async Task<IActionResult> CreateGarden([FromBody] GardenCreateModel gardenCreateModel)
    {
        if (gardenCreateModel.Users != null)
        {
            foreach (var user in gardenCreateModel.Users)
            {
                if (!ObjectId.TryParse(user.Id, out _))
                {
                    return BadRequest($"Invalid user ID: {user.Id}");
                }
            }
        }
        await _gardenService.CreateAsync(gardenCreateModel);
        return Ok("Садок створено успішно.");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Gardens/{id:length(24)}")]
    public async Task<IActionResult> UpdateGarden(string id, [FromBody] GardenUpdateModel gardenUpdateModel)
    {
        if (gardenUpdateModel.Users != null)
        {
            foreach (var user in gardenUpdateModel.Users)
            {
                if (!ObjectId.TryParse(user.Id, out _))
                {
                    return BadRequest($"Invalid user ID: {user.Id}");
                }
            }
        }
        await _gardenService.UpdateAsync(id, gardenUpdateModel);
        return Ok("Інформація про садок оновлена успішно.");
    }


    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("Gardens/{id:length(24)}")]
    public async Task<IActionResult> DeleteGarden(string id)
    {
        await _gardenService.DeleteAsync(id);
        return Ok("Садок видалено успішно.");
    }

    //[Authorize(Policy = "AdminOnly")]
    //[HttpGet("Rooms")]
    //public async Task<ActionResult<List<Room>>> GetAllRooms()
    //{
    //    var rooms = await _roomService.GetAllAsync();
    //    return Ok(rooms);
    //}

    //[Authorize(Policy = "AdminOnly")]
    //[HttpGet("Rooms/{id:length(24)}", Name = "AdminGetRoom")]
    //public async Task<ActionResult<Room>> GetRoomById(string id)
    //{
    //    var room = await _roomService.GetByIdAsync(id);
    //    if (room == null)
    //    {
    //        return NotFound("Room with this ID not found.");
    //    }
    //    return Ok(room);
    //}
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Rooms")]
    public async Task<ActionResult<List<Room>>> GetAllRooms()
    {
        var rooms = await _roomService.GetAllAsync();

        // Fetch garden names and attach to rooms
        var gardens = await _gardenService.GetAllAsync();
        var gardenDict = gardens.ToDictionary(g => g.Id, g => g.Name);
        foreach (var room in rooms)
        {
            if (gardenDict.ContainsKey(room.GardenId))
            {
                room.GardenName = gardenDict[room.GardenId];
            }
        }

        return Ok(rooms);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Rooms/{id:length(24)}", Name = "AdminGetRoom")]
    public async Task<ActionResult<Room>> GetRoomById(string id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room == null)
        {
            return NotFound("Room with this ID not found.");
        }

        var garden = await _gardenService.GetByIdAsync(room.GardenId);
        room.GardenName = garden?.Name;

        return Ok(room);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Rooms/{id:length(24)}/equipment")]
    public async Task<ActionResult<List<Equipment>>> GetEquipmentByRoomId(string id)
    {
        var equipment = await _equipmentService.GetByRoomIdAsync(id);
        return Ok(equipment);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("Rooms/{id:length(24)}")]
    public async Task<IActionResult> DeleteRoom(string id)
    {
        await _roomService.DeleteAsync(id);
        return Ok("Room deleted successfully.");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Rooms")]
    public async Task<IActionResult> CreateRoom([FromBody] RoomCreateModel roomCreateModel)
    {
        var createdRoom = await _roomService.CreateAsync(roomCreateModel);
        return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Rooms/{id:length(24)}")]
    public async Task<IActionResult> UpdateRoom(string id, [FromBody] RoomUpdateModel roomUpdateModel)
    {
        await _roomService.UpdateAsync(id, roomUpdateModel);
        return Ok("Room updated successfully.");
    }

    // Equipment Management

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Equipment")]
    public async Task<ActionResult<List<Equipment>>> GetAllEquipments()
    {
        var equipments = await _equipmentService.GetAllAsync();
        return Ok(equipments);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Equipment/{id:length(24)}", Name = "AdminGetEquipment")]
    public async Task<ActionResult<Equipment>> GetEquipmentById(string id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);
        if (equipment == null)
        {
            return NotFound("Equipment with this ID not found.");
        }
        return Ok(equipment);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Equipment/{id:length(24)}/measurements")]
    public async Task<ActionResult<List<Measurement>>> GetMeasurementsByEquipmentId(string id)
    {
        var measurements = await _measurementService.GetByDeviceIdAsync(id);
        return Ok(measurements);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("Equipment/{id:length(24)}")]
    public async Task<IActionResult> DeleteEquipment(string id)
    {
        await _equipmentService.DeleteAsync(id);
        return Ok("Equipment deleted successfully.");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Equipment")]
    public async Task<IActionResult> CreateEquipment([FromBody] EquipmentCreateModel equipmentCreateModel)
    {
        var createdEquipment = await _equipmentService.CreateAndGetAsync(equipmentCreateModel);
        return CreatedAtRoute("AdminGetEquipment", new { id = createdEquipment.Id }, createdEquipment);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Equipment/{id:length(24)}")]
    public async Task<IActionResult> UpdateEquipment(string id, [FromBody] EquipmentUpdateModel equipmentUpdateModel)
    {
        await _equipmentService.UpdateAsync(id, equipmentUpdateModel);
        return Ok("Equipment updated successfully.");
    }

    // Measurement Management

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Measurements")]
    public async Task<ActionResult<List<Measurement>>> GetAllMeasurements()
    {
        var measurements = await _measurementService.GetAllAsync();
        return Ok(measurements);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("Measurements/{id:length(24)}", Name = "AdminGetMeasurement")]
    public async Task<ActionResult<Measurement>> GetMeasurementById(string id)
    {
        var measurement = await _measurementService.GetByIdAsync(id);
        if (measurement == null)
        {
            return NotFound("Measurement with this ID not found.");
        }
        return Ok(measurement);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("Measurements/{id:length(24)}")]
    public async Task<IActionResult> DeleteMeasurement(string id)
    {
        await _measurementService.DeleteAsync(id);
        return Ok("Measurement deleted successfully.");
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Measurements")]
    public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementCreateModel measurementCreateModel)
    {
        var createdMeasurement = await _measurementService.CreateAndGetAsync(measurementCreateModel);
        if (createdMeasurement == null)
        {
            return StatusCode(500, "An error occurred while creating the measurement.");
        }

        return CreatedAtRoute("AdminGetMeasurement", new { id = createdMeasurement.Id }, createdMeasurement);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Measurements/{id:length(24)}")]
    public async Task<IActionResult> UpdateMeasurement(string id, [FromBody] MeasurementUpdateModel measurementUpdateModel)
    {
        await _measurementService.UpdateAsync(id, measurementUpdateModel);
        return Ok("Measurement updated successfully.");
    }
}
