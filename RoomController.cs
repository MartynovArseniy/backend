using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IEquipmentService _equipmentService;
    public RoomsController(IRoomService roomService, IEquipmentService equipmentService)
    {
        _roomService = roomService;
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Room>>> GetAllRooms()
    {
        var rooms = await _roomService.GetAllAsync();
        return Ok(rooms);
    }

    [HttpGet("{id:length(24)}", Name = "GetRoom")]
    public async Task<ActionResult<Room>> GetRoomById(string id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room == null)
        {
            return NotFound("Room with this ID not found.");
        }
        return Ok(room);
    }
    [HttpGet("{id:length(24)}/equipment")]
    public async Task<ActionResult<List<Equipment>>> GetEquipmentByRoomId(string id)
    {
        var equipment = await _equipmentService.GetByRoomIdAsync(id);
        return Ok(equipment);
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteRoom(string id)
    {
        await _roomService.DeleteAsync(id);
        return Ok("Room deleted successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomCreateModel roomCreateModel)
    {
        var createdRoom = await _roomService.CreateAsync(roomCreateModel);
        return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateRoom(string id, [FromBody] RoomUpdateModel roomUpdateModel)
    {
        await _roomService.UpdateAsync(id, roomUpdateModel);
        return Ok("Room updated successfully.");
    }
}

