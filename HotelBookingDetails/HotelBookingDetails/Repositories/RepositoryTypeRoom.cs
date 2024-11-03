
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryTypeRoom : IRepositoryTypeRoom
{

    private readonly List<TypeRoom> _typeRoom = [];
    private int _typeRoomId = 1;

    public bool PutTypeRoom(int id, TypeRoom typeRoom)
    {
        var oldValue = GetTypeRoomById(id);

        if (oldValue != null) { return false; }
        //доделать
        return true;
    }

    public bool DeleteTypeRoom(int id)
    {
        var typeRoom = GetTypeRoomById(id);
        if (typeRoom == null) { return false; }
        _typeRoom.Remove(typeRoom);
        return true;
    }

    public bool PostTypeRoom(TypeRoom typeRoom)
    {
        typeRoom.Id = _typeRoomId++;
        _typeRoom.Add(typeRoom);
        return true;
    }

    public TypeRoom? GetTypeRoomById(int id) => _typeRoom.Find(t => t.Id == id);

    public IEnumerable<TypeRoom> GetTypeRooms() => _typeRoom;
}
