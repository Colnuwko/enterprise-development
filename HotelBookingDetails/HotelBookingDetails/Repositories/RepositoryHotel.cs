using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryHotel(HotelBookingDbContext hotelBookingDbContext) : IRepository<Hotel>
{
    public bool Put(int id, Hotel hotel)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Address = hotel.Address;
        oldValue.City = hotel.City;
        oldValue.Name = hotel.Name;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var hotel = GetById(id);
        if (hotel == null)
            return false;
        hotelBookingDbContext.Hotels.Remove(hotel);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(Hotel hotel)
    { 
        hotelBookingDbContext.Hotels.Add(hotel);
        hotelBookingDbContext.SaveChanges();
    }

    public Hotel? GetById(int id) => hotelBookingDbContext.Hotels.FirstOrDefault(h => h.Id == id);

    public IEnumerable<Hotel> GetAll() => hotelBookingDbContext.Hotels;
}