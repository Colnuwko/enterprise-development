

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryHotel : IRepositoryHotel
{
    private readonly List<Hotel> _hotels = [];
    private int _hotelId = 1;

    public bool PutHotel(int id, Hotel hotel)
    {
        var oldValue = GetHotelById(id);

        oldValue.Address = hotel.Address;
        oldValue.City = hotel.City;
        oldValue.Name = hotel.Name;
        return true;
    }

    public bool DeleteHotel(int id)
    {
        var hotel = GetHotelById(id);
        if (hotel == null) { return false; }
        _hotels.Remove(hotel);
        return true;
    }

    public bool PostHotel(Hotel hotel)
    {
        hotel.Id = _hotelId++;
        _hotels.Add(hotel);
        return true;
    }

    public Hotel? GetHotelById(int id) => _hotels.Find(h => h.Id == id);

    public IEnumerable<Hotel> GetHotels() => _hotels;

    public int GetCountHotels() => _hotels.Count;

    public int GetHotelIdByName(string name)
    {
        return _hotels.Where(h => h.Name == name).Select(h => h.Id).First();
    }


    public IEnumerable<Hotel> GetTopFiveHotelById(IEnumerable<int> id)
    {
        return _hotels.Where(h => id.Contains(h.Id));
    }
}
