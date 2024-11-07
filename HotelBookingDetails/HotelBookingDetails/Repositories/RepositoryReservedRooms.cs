
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryReservedRooms : IRepository<ReservedRooms>
{
    private readonly List<ReservedRooms> _reservedRooms = [];
    private int _reservedRoomId = 1;

    public bool Put(int id, ReservedRooms reservedRooms)
    {
        var oldValue = GetById(id);
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

    public bool Post(ReservedRooms reservedRooms)
    {
        reservedRooms.Id = _reservedRoomId++;
        _reservedRooms.Add(reservedRooms);
        return true;
    }

    public ReservedRooms? GetById(int id) => _reservedRooms.Find(r => r.Id == id);

    public IEnumerable<ReservedRooms> GetAll() => _reservedRooms;

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


    public IEnumerable<Room> GetFreeRoomInCity(IEnumerable<Room> rooms)
    {
        var reservRooms = _reservedRooms.Where(r => rooms.Contains(r.Room) && r.DateDeparture == null).Select(r => r.Room);
        var freeRooms = rooms.Where(r => !reservRooms.Contains(r)).Select(r => r);
        return freeRooms;
    }

    public IEnumerable<Client> GetLongLiversHotel()
    {
        var longerPeriods = _reservedRooms
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Max(c => c.Total);

        var clientWithLongerPer = _reservedRooms
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Where(c => c.Total == longerPeriods).Select(c => c.Client).ToList();
        return clientWithLongerPer;
    }
}
