using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryRoom(HotelBookingDbContext hotelBookingDbContext) : IRepository<Room>
{
    public bool Put(int id, Room room)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Hotel = room.Hotel;
        oldValue.Cost = room.Cost;
        oldValue.Capacity = room.Capacity;
        oldValue.Type = room.Type;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var room = GetById(id);
        if (room == null)
            return false;
        hotelBookingDbContext.Rooms.Remove(room);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(Room room)
    {
        hotelBookingDbContext.Rooms.Add(room);
        hotelBookingDbContext.SaveChanges();
    }

    public Room? GetById(int id) => hotelBookingDbContext.Rooms.Include(r => r.Type).Include(r => r.Hotel).FirstOrDefault(r => r.Id == id);

    public IEnumerable<Room> GetAll() => hotelBookingDbContext.Rooms.Include(r => r.Type).Include(r => r.Hotel);
}
