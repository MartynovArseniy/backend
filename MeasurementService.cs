using MongoDB.Driver;

public interface IMeasurementService
{
    Task<List<Measurement>> GetAllAsync();
    Task<Measurement> GetByIdAsync(string id);
    Task CreateAsync(MeasurementCreateModel measurementCreateModel);
    Task<Measurement> CreateAndGetAsync(MeasurementCreateModel measurementCreateModel);
    Task UpdateAsync(string id, MeasurementUpdateModel updateModel);
    Task DeleteAsync(string id);
    Task<List<Measurement>> GetByDeviceIdAsync(string deviceId);
}


public class MeasurementService : IMeasurementService
{
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementService(IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    public async Task<List<Measurement>> GetAllAsync()
    {
        return await _measurementRepository.GetAllAsync();
    }

    public async Task<Measurement> GetByIdAsync(string id)
    {
        return await _measurementRepository.GetByIdAsync(id);
    }

    public async Task<List<Measurement>> GetByDeviceIdAsync(string deviceId)
    {
        return await _measurementRepository.GetByDeviceIdAsync(deviceId);
    }
    public async Task<Measurement> CreateAndGetAsync(MeasurementCreateModel measurementCreateModel)
    {
        var measurement = new Measurement
        {
            DeviceId = measurementCreateModel.DeviceId,
            MeasurementType = measurementCreateModel.MeasurementType,
            Value = measurementCreateModel.Value
        };
        await _measurementRepository.CreateAsync(measurement);
        return measurement;
    }

    public async Task CreateAsync(MeasurementCreateModel measurementCreateModel)
    {
        var measurement = new Measurement
        {
            DeviceId = measurementCreateModel.DeviceId,
            MeasurementType = measurementCreateModel.MeasurementType,
            Value = measurementCreateModel.Value
        };
        await _measurementRepository.CreateAsync(measurement);
    }

    public async Task UpdateAsync(string id, MeasurementUpdateModel updateModel)
    {
        var update = Builders<Measurement>.Update
            .Set(m => m.DeviceId, updateModel.DeviceId)
            .Set(m => m.MeasurementType, updateModel.MeasurementType)
            .Set(m => m.Value, updateModel.Value);
        await _measurementRepository.UpdateAsync(id, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _measurementRepository.DeleteAsync(id);
    }
}
