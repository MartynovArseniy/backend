public class RoomCreateModel
{
    public string RoomNumber { get; set; }
    public string Capacity { get; set; }
    public string GardenId { get; set; }
}

public class RoomUpdateModel
{
    public string RoomNumber { get; set; }
    public string Capacity { get; set; } // Тепер це рядок
    public string GardenId { get; set; }
}