
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryRoom : IRepositoryRoom
{
    private readonly List<Room> _rooms = [];
    private int _roomId = 1;

    public bool PutRoom(int id, Room client)
    {
        var oldValue = GetRoomById(id);

        if (oldValue != null) { return false; }
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

    public Room? GetRoomById(int id) => _rooms.Find(c => c.Id == id);

    public IEnumerable<Room> GetRooms() => _rooms;

}
