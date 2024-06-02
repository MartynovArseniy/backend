using MongoDB.Driver;

public interface IRoomService
{
    Task<List<Room>> GetAllAsync();
    Task<Room> GetByIdAsync(string id);
    Task<Room> CreateAsync(RoomCreateModel roomCreateModel);
    Task UpdateAsync(string id, RoomUpdateModel updateModel);
    Task DeleteAsync(string id);
    Task<List<Room>> GetRoomsByGardenIdAsync(string gardenId);
}


//public class RoomService : IRoomService
//{
//    private readonly IRoomRepository _roomRepository;

//    public RoomService(IRoomRepository roomRepository)
//    {
//        _roomRepository = roomRepository;
//    }

//    public async Task<List<Room>> GetAllAsync()
//    {
//        return await _roomRepository.GetAllAsync();
//    }

//    public async Task<Room> GetByIdAsync(string id)
//    {
//        return await _roomRepository.GetByIdAsync(id);
//    }

//    public async Task<Room> CreateAsync(RoomCreateModel roomCreateModel)
//    {
//        var room = new Room
//        {
//            RoomNumber = roomCreateModel.RoomNumber,
//            Capacity = roomCreateModel.Capacity,
//            GardenId = roomCreateModel.GardenId
//        };
//        await _roomRepository.CreateAsync(room);
//        return room;
//    }

//    public async Task UpdateAsync(string id, RoomUpdateModel updateModel)
//    {
//        var update = Builders<Room>.Update
//            .Set(r => r.RoomNumber, updateModel.RoomNumber)
//            .Set(r => r.Capacity, updateModel.Capacity)
//            .Set(r => r.GardenId, updateModel.GardenId);
//        await _roomRepository.UpdateAsync(id, update);
//    }

//    public async Task DeleteAsync(string id)
//    {
//        await _roomRepository.DeleteAsync(id);
//    }
//}

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<Room>> GetAllAsync()
    {
        return await _roomRepository.GetAllAsync();
    }

    public async Task<Room> GetByIdAsync(string id)
    {
        return await _roomRepository.GetByIdAsync(id);
    }

    public async Task<Room> CreateAsync(RoomCreateModel roomCreateModel)
    {
        var room = new Room
        {
            RoomNumber = roomCreateModel.RoomNumber,
            Capacity = roomCreateModel.Capacity,
            GardenId = roomCreateModel.GardenId
        };
        await _roomRepository.CreateAsync(room);
        return room;
    }

    public async Task UpdateAsync(string id, RoomUpdateModel updateModel)
    {
        var update = Builders<Room>.Update
            .Set(r => r.RoomNumber, updateModel.RoomNumber)
            .Set(r => r.Capacity, updateModel.Capacity)
            .Set(r => r.GardenId, updateModel.GardenId);
        await _roomRepository.UpdateAsync(id, update);
    }

    public async Task DeleteAsync(string id)
    {
        await _roomRepository.DeleteAsync(id);
    }

    public async Task<List<Room>> GetRoomsByGardenIdAsync(string gardenId)
    {
        return await _roomRepository.GetRoomsByGardenIdAsync(gardenId);
    }
}
