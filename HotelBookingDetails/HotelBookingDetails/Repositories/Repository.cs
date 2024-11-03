
namespace HotelBookingDetails.Domain.Repositories;

public class Repository : IRepository
{
    private readonly List<Client> _clients = [];
    private readonly List<Hotel> _hotels = [];
    private readonly List<Passport> _passports = [];
    private readonly List<ReservedRooms> _reservedRooms = [];
    private readonly List<Room> _room = [];
    private readonly List<TypeRoom> _typeRoom = [];
    private int _clientId = 1;

    public bool PutClient(int id, Client client)
    {
        var oldValue = GetClientById(id);

        if (oldValue != null) { return false; }
        oldValue.FullName = client.FullName;
        oldValue.PassportData = client.PassportData;
        oldValue.Birthday = client.Birthday;
        return true;
    }

    public bool DeleteClient(int id)
    {
        var client = GetClientById(id);
        if (client != null) { return false; }
        _clients.Remove(client);
        return true;
    }

    public bool PostClient(Client client)
    {
        client.Id = _clientId++;
        _clients.Add(client);
        return true;
    }

    public Client? GetClientById(int id) => _clients.Find(c => c.Id == id);

    public IEnumerable<Client> GetClients() => _clients;

    public bool DeleteHotel(int id)
    {
        throw new NotImplementedException();
    }

    public bool DeletePassport(int id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteReservedRooms(int id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteRoom(int id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteTypeRoom(int id)
    {
        throw new NotImplementedException();
    }

    public Hotel? GetHotelById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Hotel> GetHotels()
    {
        throw new NotImplementedException();
    }

    public Passport? GetPassportById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Passport> GetPassports()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ReservedRooms> GetReservedRooms()
    {
        throw new NotImplementedException();
    }

    public ReservedRooms? GetReservedRoomsById(int id)
    {
        throw new NotImplementedException();
    }

    public Room? GetRoomById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Room> GetRooms()
    {
        throw new NotImplementedException();
    }

    public TypeRoom? GetTypeRoomById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TypeRoom> GetTypeRooms()
    {
        throw new NotImplementedException();
    }

    public bool PostHotel(Hotel client)
    {
        throw new NotImplementedException();
    }

    public bool PostPassport(Passport client)
    {
        throw new NotImplementedException();
    }

    public bool PostReservedRooms(ReservedRooms client)
    {
        throw new NotImplementedException();
    }

    public bool PostRoom(Room client)
    {
        throw new NotImplementedException();
    }

    public bool PostTypeRoom(TypeRoom client)
    {
        throw new NotImplementedException();
    }

    public bool PutHotel(int id, Hotel client)
    {
        throw new NotImplementedException();
    }

    public bool PutPassport(int id, Passport client)
    {
        throw new NotImplementedException();
    }

    public bool PutReservedRooms(int id, ReservedRooms client)
    {
        throw new NotImplementedException();
    }

    public bool PutRoom(int id, Room client)
    {
        throw new NotImplementedException();
    }

    public bool PutTypeRoom(int id, TypeRoom client)
    {
        throw new NotImplementedException();
    }
}
