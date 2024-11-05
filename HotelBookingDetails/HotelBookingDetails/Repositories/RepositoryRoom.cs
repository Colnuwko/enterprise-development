
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryRoom : IRepositoryRoom
{
    private readonly List<Room> _rooms = [];
    private int _roomId = 1;

    public bool PutRoom(int id, Room room)
    {
        var oldValue = GetRoomById(id);

        oldValue.HotelId = room.HotelId;
        oldValue.Cost = room.Cost;
        oldValue.Capacity = room.Capacity;
        oldValue.Type = room.Type;
        return true;
    }

    public bool DeleteRoom(int id)
    {
        var room = GetRoomById(id);
        if (room == null) { return false; }
        _rooms.Remove(room);
        return true;
    }

    public bool PostRoom(Room room)
    {
        room.Id = _roomId++;
        _rooms.Add(room);
        return true;
    }

    public Room? GetRoomById(int id) => _rooms.Find(r => r.Id == id);

    public IEnumerable<Room> GetRooms() => _rooms;

    public IEnumerable<Room> GetRoomsInHotel(IEnumerable<int> id) => _rooms.Where(r => id.Contains(r.HotelId)).Select(r => r);

}
