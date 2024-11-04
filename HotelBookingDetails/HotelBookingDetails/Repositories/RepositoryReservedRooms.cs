
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryReservedRooms : IRepositoryReservedRooms
{
    private readonly List<ReservedRooms> _reservedRooms = [];
    private int _reservedRoomId = 1;

    public bool PutReservedRoom(int id, ReservedRooms reservedRooms)
    {
        var oldValue = GetReservedRoomById(id);
        oldValue.DateDeparture = reservedRooms.DateDeparture;
        oldValue.Period = reservedRooms.Period;
        oldValue.DateArrival = reservedRooms.DateArrival;
        oldValue.Client = reservedRooms.Client;
        oldValue.Room = reservedRooms.Room;

        return true;
    }

    public bool DeleteReservedRoom(int id)
    {
        var reservedRoom = GetReservedRoomById(id);
        if (reservedRoom == null) { return false; }
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

    public IEnumerable<Client> ReturnAllClientInHotel(int hotelId, IEnumerable<Room> rooms)
    {
        var clientInHotel = _reservedRooms
            .OrderBy(r => r.Client.FullName)
            .Where(r => rooms.ToList().Contains(r.Room))
            .Select(r => r.Client)
            .ToList();
        return clientInHotel;
    }

    public IEnumerable<int> GetTopFiveHotelId() => _reservedRooms.GroupBy(r => r.Room.HotelId).Select(r => r.Key).Take(5);

}
