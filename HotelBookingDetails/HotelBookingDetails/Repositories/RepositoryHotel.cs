

using System.Runtime.CompilerServices;
using System.Transactions;

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

    public IEnumerable<int> GetHotelsByCity(string city) => _hotels.Where(h => h.City == city).Select(h => h.Id);


    public IEnumerable<Hotel> GetTopFiveHotelById(IEnumerable<int> id)
    {
        return _hotels.Where(h => id.Contains(h.Id));
    }

    
    public IEnumerable<T> GetMaxAvgMinForHotels(IEnumerable<Room> rooms)
    {

        var hotelCosts = _hotels.Select(h => new T
        ( 
            (_hotels.Where(hotel => hotel.Id == h.Id).Select(hotel => hotel)),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Min(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Max(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Average(rm => rm.Cost)
        )).AsEnumerable();

        return hotelCosts;
    }
}
