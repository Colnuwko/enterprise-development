
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryRoom : IRepository<Room>
{
    private readonly List<Room> _rooms = [];
    private int _roomId = 1;

    public bool Put(int id, Room room)
    {
        var oldValue = GetById(id);

        oldValue.HotelId = room.HotelId;
        oldValue.Cost = room.Cost;
        oldValue.Capacity = room.Capacity;
        oldValue.Type = room.Type;
        return true;
    }

    public bool Delete(int id)
    {
        var room = GetById(id);
        if (room == null) 
            return false; 
        _rooms.Remove(room);
        return true;
    }

    public bool Post(Room room)
    {
        room.Id = _roomId++;
        _rooms.Add(room);
        return true;
    }

    public Room? GetById(int id) => _rooms.Find(r => r.Id == id);

    public IEnumerable<Room> GetAll() => _rooms;

    public IEnumerable<Room> GetRoomsInHotel(IEnumerable<int> id) => _rooms.Where(r => id.Contains(r.HotelId)).Select(r => r);

}
