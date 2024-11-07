namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryTypeRoom : IRepository<TypeRoom>
{

    private readonly List<TypeRoom> _typeRoom = [];
    private int _typeRoomId = 1;

    public bool Put(int id, TypeRoom typeRoom)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Name = typeRoom.Name;
        return true;
    }

    public bool Delete(int id)
    {
        var typeRoom = GetById(id);
        if (typeRoom == null) 
            return false; 
        _typeRoom.Remove(typeRoom);
        return true;
    }

    public bool Post(TypeRoom typeRoom)
    {
        typeRoom.Id = _typeRoomId++;
        _typeRoom.Add(typeRoom);
        return true;
    }

    public TypeRoom? GetById(int id) => _typeRoom.Find(t => t.Id == id);

    public IEnumerable<TypeRoom> GetAll() => _typeRoom;
}