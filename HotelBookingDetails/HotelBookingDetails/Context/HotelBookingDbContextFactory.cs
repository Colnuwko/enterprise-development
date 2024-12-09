using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace HotelBookingDetails.Domain.Context;
public class HotelBookingDbContextFactory : IDesignTimeDbContextFactory<HotelBookingDbContext>
{
    public HotelBookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelBookingDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Booking;Username=postgres;Password=postgres");
        return new HotelBookingDbContext(optionsBuilder.Options);
    }
}