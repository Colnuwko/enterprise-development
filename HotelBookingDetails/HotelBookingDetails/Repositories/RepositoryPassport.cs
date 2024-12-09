using HotelBookingDetails.Domain.Context;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryPassport(HotelBookingDbContext hotelBookingDbContext) : IRepository<Passport>
{
    public bool Put(int id, Passport passport)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.Number = passport.Number;
        oldValue.Series = passport.Series;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var passport = GetById(id);
        if (passport == null) 
            return false;
        hotelBookingDbContext.Passports.Remove(passport);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(Passport passport)
    {
        hotelBookingDbContext.Passports.Add(passport);
        hotelBookingDbContext.SaveChanges();
    }

    public Passport? GetById(int id) => hotelBookingDbContext.Passports.FirstOrDefault(p => p.Id == id);

    public IEnumerable<Passport> GetAll() => hotelBookingDbContext.Passports;
}