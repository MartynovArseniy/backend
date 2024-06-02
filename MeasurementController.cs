using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.Metrics;

//[Route("api/[controller]")]
//[ApiController]
//public class MeasurementsController : ControllerBase
//{
//    private readonly IMongoCollection<Measurement> _measurementsCollection;

//    public MeasurementsController(IMongoClient client)
//    {
//        var database = client.GetDatabase("ChildClimaCare");
//        _measurementsCollection = database.GetCollection<Measurement>("Measurement");
//    }

//    [HttpGet]
//    public async Task<ActionResult<List<Measurement>>> GetAllMeasurements()
//    {
//        try
//        {
//            var measurements = await _measurementsCollection.Find(_ => true).ToListAsync();
//            if (measurements == null || measurements.Count == 0)
//            {
//                // Логування, якщо список вимірювань порожній
//                Console.WriteLine("Колекція 'Measurement' порожня або не існує.");
//            }
//            return measurements;
//        }
//        catch (Exception ex)
//        {
//            // Логування помилки
//            Console.WriteLine($"Виникла помилка: {ex.Message}");
//            return StatusCode(500, "Помилка сервера при отриманні вимірювань.");
//        }
//    }

//    [HttpGet("{id:length(24)}", Name = "GetMeasurement")]
//    public async Task<ActionResult<Measurement>> GetMeasurementById(string id)
//    {
//        var measurement = await _measurementsCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
//        if (measurement == null)
//        {
//            return NotFound("Измерение с таким ID не найдено.");
//        }
//        return measurement;
//    }

//    [HttpDelete("{id:length(24)}")]
//    public async Task<IActionResult> DeleteMeasurement(string id)
//    {
//        var filter = Builders<Measurement>.Filter.Eq(m => m.Id, id);
//        var result = await _measurementsCollection.DeleteOneAsync(filter);

//        if (result.DeletedCount == 0)
//        {
//            return NotFound("Измерение с таким ID не найдено.");
//        }

//        return Ok("Измерение удалено успешно.");
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementCreateModel measurementCreateModel)
//    {
//        var measurement = new Measurement
//        {
//            DeviceId = measurementCreateModel.DeviceId,
//            MeasurementType = measurementCreateModel.MeasurementType,
//            Value = measurementCreateModel.Value
//        };

//        await _measurementsCollection.InsertOneAsync(measurement);
//        return CreatedAtAction(nameof(GetMeasurementById), new { id = measurement.Id }, measurement);
//    }

//    [HttpPut("{id:length(24)}")]
//    public async Task<IActionResult> UpdateMeasurement(string id, [FromBody] MeasurementUpdateModel measurementUpdateModel)
//    {
//        var filter = Builders<Measurement>.Filter.Eq(m => m.Id, id);
//        var update = Builders<Measurement>.Update
//            .Set(m => m.DeviceId, measurementUpdateModel.DeviceId)
//            .Set(m => m.MeasurementType, measurementUpdateModel.MeasurementType)
//            .Set(m => m.Value, measurementUpdateModel.Value);

//        var result = await _measurementsCollection.UpdateOneAsync(filter, update);

//        if (result.ModifiedCount == 0)
//        {
//            return NotFound("Измерение с таким ID не найдено.");
//        }

//        return Ok("Информация об измерении обновлена успешно.");
//    }

//    // Добавьте здесь другие методы API...
//}
[Route("api/[controller]")]
[ApiController]
public class MeasurementsController : ControllerBase
{
    private readonly IMeasurementService _measurementService;

    public MeasurementsController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Measurement>>> GetAllMeasurements()
    {
        var measurements = await _measurementService.GetAllAsync();
        return Ok(measurements);
    }

    [HttpGet("{id:length(24)}", Name = "GetMeasurement")]
    public async Task<ActionResult<Measurement>> GetMeasurementById(string id)
    {
        var measurement = await _measurementService.GetByIdAsync(id);
        if (measurement == null)
        {
            return NotFound("Measurement with this ID not found.");
        }
        return Ok(measurement);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteMeasurement(string id)
    {
        await _measurementService.DeleteAsync(id);
        return Ok("Measurement deleted successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementCreateModel measurementCreateModel)
    {
        var createdMeasurement = await _measurementService.CreateAndGetAsync(measurementCreateModel);
        if (createdMeasurement == null)
        {
            return StatusCode(500, "An error occurred while creating the measurement.");
        }

        return CreatedAtRoute("GetMeasurement", new { id = createdMeasurement.Id }, createdMeasurement);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateMeasurement(string id, [FromBody] MeasurementUpdateModel measurementUpdateModel)
    {
        await _measurementService.UpdateAsync(id, measurementUpdateModel);
        return Ok("Measurement updated successfully.");
    }
}



//public class Measurement
//{
//    [BsonId]
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string Id { get; set; }
//    public string DeviceId { get; set; }
//    public string MeasurementType { get; set; }
//    public string Value { get; set; }
//}

////public class MeasurementCreateModel
////{
////    public string DeviceId { get; set; }
////    public string MeasurementType { get; set; }
////    public string Value { get; set; }
////}

//public class MeasurementUpdateModel
//{
//    public string DeviceId { get; set; }
//    public string MeasurementType { get; set; }
//    public string Value { get; set; }
//}