using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;
    private readonly IMeasurementService _measurementService;
    public EquipmentController(IEquipmentService equipmentService, IMeasurementService measurementService)
    {
        _equipmentService = equipmentService;
        _measurementService = measurementService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Equipment>>> GetAllEquipments()
    {
        var equipments = await _equipmentService.GetAllAsync();
        return Ok(equipments);
    }

    [HttpGet("{id:length(24)}", Name = "GetEquipment")]
    public async Task<ActionResult<Equipment>> GetEquipmentById(string id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);
        if (equipment == null)
        {
            return NotFound("Equipment with this ID not found.");
        }
        return Ok(equipment);
    }
    [HttpGet("{id:length(24)}/measurements")]
    public async Task<ActionResult<List<Measurement>>> GetMeasurementsByEquipmentId(string id)
    {
        var measurements = await _measurementService.GetByDeviceIdAsync(id);
        return Ok(measurements);
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteEquipment(string id)
    {
        await _equipmentService.DeleteAsync(id);
        return Ok("Equipment deleted successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateEquipment([FromBody] EquipmentCreateModel equipmentCreateModel)
    {
        // Create the equipment
        var createdEquipment = await _equipmentService.CreateAndGetAsync(equipmentCreateModel);

        // Return the created equipment with the generated ID
        return CreatedAtRoute("GetEquipment", new { id = createdEquipment.Id }, createdEquipment);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateEquipment(string id, [FromBody] EquipmentUpdateModel equipmentUpdateModel)
    {
        await _equipmentService.UpdateAsync(id, equipmentUpdateModel);
        return Ok("Equipment updated successfully.");
    }
}


