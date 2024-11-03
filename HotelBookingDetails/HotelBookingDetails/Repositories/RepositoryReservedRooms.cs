
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryReservedRooms : IRepositoryReservedRooms
{
    private readonly List<ReservedRooms> _reservedRooms = [];
    private int _reservedRoomId = 1;

    public bool PutReservedRoom(int id, ReservedRooms client)
    {
        var oldValue = GetReservedRoomById(id);

        if (oldValue != null) { return false; }
        // доделать
        return true;
    }

    public bool DeleteReservedRoom(int id)
    {
        var reservedRoom = GetReservedRoomById(id);
        if (reservedRoom != null) { return false; }
        _reservedRooms.Remove(reservedRoom);
        return true;
    }

    public bool PostReservedRoom(ReservedRooms reservedRooms)
    {
        reservedRooms.Id = _reservedRoomId++;
        _reservedRooms.Add(reservedRooms);
        return true;
    }

    public ReservedRooms? GetReservedRoomById(int id) => _reservedRooms.Find(r => r.Id == id);

    public IEnumerable<ReservedRooms> GetReservedRooms() => _reservedRooms;
}
