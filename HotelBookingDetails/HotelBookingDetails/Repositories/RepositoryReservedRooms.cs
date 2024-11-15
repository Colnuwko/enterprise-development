namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryReservedRooms : IRepository<ReservedRooms>
{
    private readonly List<ReservedRooms> _reservedRooms = [];
    private int _reservedRoomId = 1;

    public bool Put(int id, ReservedRooms reservedRooms)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.DateDeparture = reservedRooms.DateDeparture;
        oldValue.Period = reservedRooms.Period;
        oldValue.DateArrival = reservedRooms.DateArrival;
        oldValue.Client = reservedRooms.Client;
        oldValue.Room = reservedRooms.Room;
        return true;
    }

    public bool Delete(int id)
    {
        var reservedRoom = GetById(id);
        if (reservedRoom == null) 
            return false; 
        _reservedRooms.Remove(reservedRoom);
        return true;
    }

    public void Post(ReservedRooms reservedRooms)
    {
        reservedRooms.Id = _reservedRoomId++;
        _reservedRooms.Add(reservedRooms);
    }

    public ReservedRooms? GetById(int id) => _reservedRooms.Find(r => r.Id == id);

    public IEnumerable<ReservedRooms> GetAll() => _reservedRooms;
}