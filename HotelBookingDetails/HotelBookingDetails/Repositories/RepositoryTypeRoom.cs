using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryTypeRoom(HotelBookingDbContext hotelBookingDbContext) : IRepository<TypeRoom>
{
    public bool Put(int id, TypeRoom typeRoom)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Name = typeRoom.Name;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var typeRoom = GetById(id);
        if (typeRoom == null) 
            return false;
        hotelBookingDbContext.TypeRooms.Remove(typeRoom);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(TypeRoom typeRoom)
    {
        hotelBookingDbContext.TypeRooms.Add(typeRoom);
        hotelBookingDbContext.SaveChanges();
    }

    public TypeRoom? GetById(int id) => hotelBookingDbContext.TypeRooms.FirstOrDefault(t => t.Id == id);

    public IEnumerable<TypeRoom> GetAll() => hotelBookingDbContext.TypeRooms;
}