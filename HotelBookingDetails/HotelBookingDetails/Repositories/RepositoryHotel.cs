namespace HotelBookingDetails.Domain.Repositories;

public class T
{
    public T(IEnumerable<Hotel> hotel, int min, int max, double avg)
    {
        Min = min; Max = max;
        Avg = avg;
        Hotel = hotel;
    }
    public IEnumerable<Hotel> Hotel { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
    public double Avg { get; set; }
}

public class RepositoryHotel : IRepository<Hotel>
{
    private readonly List<Hotel> _hotels = [];
    private int _hotelId = 1;

    public bool Put(int id, Hotel hotel)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Address = hotel.Address;
        oldValue.City = hotel.City;
        oldValue.Name = hotel.Name;
        return true;
    }

    public bool Delete(int id)
    {
        var hotel = GetById(id);
        if (hotel == null)
            return false; 
        _hotels.Remove(hotel);
        return true;
    }

    public bool Post(Hotel hotel)
    {
        hotel.Id = _hotelId++;
        _hotels.Add(hotel);
        return true;
    }

    public Hotel? GetById(int id) => _hotels.Find(h => h.Id == id);

    public IEnumerable<Hotel> GetAll() => _hotels;
}