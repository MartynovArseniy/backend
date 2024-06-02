using MongoDB.Driver;
using static EquipmentController;

public interface IEquipmentService
{
    Task<List<Equipment>> GetAllAsync();
    Task<Equipment> GetByIdAsync(string id);
    Task CreateAsync(EquipmentCreateModel equipmentCreateModel);
    Task<Equipment> CreateAndGetAsync(EquipmentCreateModel equipmentCreateModel);
    Task UpdateAsync(string id, EquipmentUpdateModel updateModel);
    Task DeleteAsync(string id);
    Task<List<Equipment>> GetByRoomIdAsync(string roomId);
}


public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<List<Equipment>> GetAllAsync()
    {
        return await _equipmentRepository.GetAllAsync();
    }

    public async Task<Equipment> GetByIdAsync(string id)
    {
        return await _equipmentRepository.GetByIdAsync(id);
    }

    public async Task<List<Equipment>> GetByRoomIdAsync(string roomId)
    {
        return await _equipmentRepository.GetByRoomIdAsync(roomId);
    }

    public async Task<Equipment> CreateAndGetAsync(EquipmentCreateModel equipmentCreateModel)
    {
        var equipment = new Equipment
        {
            RoomId = equipmentCreateModel.RoomId,
            Name = equipmentCreateModel.Name,
            Status = equipmentCreateModel.Status
        };
        await _equipmentRepository.CreateAsync(equipment);
        return equipment;
    }


    public async Task CreateAsync(EquipmentCreateModel equipmentCreateModel)
    {
        var equipment = new Equipment
        {
            RoomId = equipmentCreateModel.RoomId,
            Name = equipmentCreateModel.Name,
            Status = equipmentCreateModel.Status
        };
        await _equipmentRepository.CreateAsync(equipment);
    }

    public async Task UpdateAsync(string id, EquipmentUpdateModel updateModel)
    {
        var update = Builders<Equipment>.Update
            .Set(e => e.RoomId, updateModel.RoomId)
            .Set(e => e.Name, updateModel.Name)
            .Set(e => e.Status, updateModel.Status);
        await _equipmentRepository.UpdateAsync(id, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _equipmentRepository.DeleteAsync(id);
    }
}
