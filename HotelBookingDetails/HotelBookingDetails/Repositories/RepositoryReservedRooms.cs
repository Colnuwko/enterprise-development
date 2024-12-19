using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryReservedRooms(HotelBookingDbContext hotelBookingDbContext) : IRepository<ReservedRoom>
{
    public bool Put(int id, ReservedRoom reservedRooms)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.DateDeparture = reservedRooms.DateDeparture;
        oldValue.Period = reservedRooms.Period;
        oldValue.DateArrival = reservedRooms.DateArrival;
        oldValue.Client = reservedRooms.Client;
        oldValue.Room = reservedRooms.Room;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var reservedRoom = GetById(id);
        if (reservedRoom == null)
            return false;
        hotelBookingDbContext.ReservedRooms.Remove(reservedRoom);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(ReservedRoom reservedRooms)
    {
        hotelBookingDbContext.ReservedRooms.Add(reservedRooms);
        hotelBookingDbContext.SaveChanges();
    }

    public ReservedRoom? GetById(int id)
    {
        return hotelBookingDbContext.ReservedRooms
            .Include(rr => rr.Client)
            .ThenInclude(c => c.PassportData)
            .Include(rr => rr.Room)
            .ThenInclude(r => r.Type)
            .Include(rr => rr.Room.Hotel)
            .FirstOrDefault(r => r.Id == id);
    }
    public IEnumerable<ReservedRoom> GetAll()
    {
        return hotelBookingDbContext.ReservedRooms
            .Include(rr => rr.Client)
            .ThenInclude(c => c.PassportData)
            .Include(rr => rr.Room)
            .ThenInclude(r => r.Type)
            .Include(rr => rr.Room.Hotel);
    }
}