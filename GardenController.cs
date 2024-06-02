using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

[Route("api/[controller]")]
[ApiController]
public class GardensController : ControllerBase
{
    private readonly IGardenService _gardenService;
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IRoomService _roomService;
    public GardensController(IGardenService gardenService, IMongoClient client, IRoomService roomService)
    {
        _gardenService = gardenService;
        var database = client.GetDatabase("ChildClimaCare");
        _usersCollection = database.GetCollection<User>("User");
        _roomService = roomService;
    }


    [HttpGet("Users/{gardenId}")]
    public async Task<ActionResult<List<object>>> GetUsersByGarden(string gardenId)
    {
        var garden = await _gardenService.GetByIdAsync(gardenId);
        if (garden == null)
        {
            return NotFound("Садок не знайдено.");
        }

        var usersIds = garden.Users.Select(u => u.Id).ToList();
        var objectIdUsersIds = usersIds.Select(id => new ObjectId(id)).ToList();
        var users = await _usersCollection.Find(u => objectIdUsersIds.Contains(u.Id)).ToListAsync();

        var userResponses = users.Select(u => new
        {
            //UserId = u.UserId,
            u.Username,
            u.FirstName,
            u.LastName,
            u.Email,
            u.Phone,
            u.Role,
            u.Status
        }).ToList();

        return Ok(userResponses);
    }
    [HttpGet("{gardenId}/rooms")]
    public async Task<ActionResult<List<Room>>> GetRoomsByGardenId(string gardenId)
    {
        var rooms = await _roomService.GetRoomsByGardenIdAsync(gardenId);
        return Ok(rooms);
    }
    [HttpPost]
    public async Task<IActionResult> CreateGarden([FromBody] GardenCreateModel gardenCreateModel)
    {
        // Проверка валидности идентификаторов пользователей
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


    [HttpGet]
    public async Task<ActionResult<List<Garden>>> GetAllGardens()
    {
        var gardens = await _gardenService.GetAllAsync();
        return Ok(gardens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Garden>> GetGarden(string id)
    {
        var garden = await _gardenService.GetByIdAsync(id);
        if (garden == null)
        {
            return NotFound();
        }
        return Ok(garden);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGarden(string id, [FromBody] GardenUpdateModel gardenUpdateModel)
    {
        await _gardenService.UpdateAsync(id, gardenUpdateModel);
        return Ok("Інформація про садок оновлена успішно.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGarden(string id)
    {
        await _gardenService.DeleteAsync(id);
        return Ok("Садок видалено успішно.");
    }
}



public class GardenUpdateModel
{
        public string Name { get; set; }
        public string Location { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Список користувачів може бути необов'язковим
        public List<UserReference> Users { get; set; } = new List<UserReference>();
}

    public class GardenCreateModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Зробіть список користувачів необов'язковим
        public List<UserReference> Users { get; set; } = new List<UserReference>();
    }

    public class Garden
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserReference> Users { get; set; }
    }


public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public ObjectId Id { get; set; }

    [BsonIgnore]
    public string UserId => Id.ToString();

    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
}


