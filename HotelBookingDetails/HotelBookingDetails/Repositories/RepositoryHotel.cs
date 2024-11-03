

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryHotel : IRepositoryHotel
{
    private readonly List<Hotel> _hotels = [];
    private int _hotelId = 1;

    public bool PutHotel(int id, Hotel hotel)
    {
        var oldValue = GetHotelById(id);

        if (oldValue != null) { return false; }
        // сделать
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
}
