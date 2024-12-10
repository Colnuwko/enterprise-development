using HotelBookingDetails.Domain.Entity;
using HotelBookingDetails.Domain.Context;
using Microsoft.EntityFrameworkCore;
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryClient(HotelBookingDbContext hotelBookingDbContext) : IRepository<Client>
{
    public bool Put(int id, Client client)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.FullName = client.FullName;
        oldValue.PassportData = client.PassportData;
        oldValue.Birthday = client.Birthday;
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var client = GetById(id);
        if (client == null) 
            return false;
        hotelBookingDbContext.Clients.Remove(client);
        hotelBookingDbContext.SaveChanges();
        return true;
    }

    public void Post(Client client)
    {
        hotelBookingDbContext.Clients.Add(client);
        hotelBookingDbContext.SaveChanges();
    }

    public Client? GetById(int id) => hotelBookingDbContext.Clients.Include(c => c.PassportData).FirstOrDefault(c => c.Id == id);

    public IEnumerable<Client> GetAll() => hotelBookingDbContext.Clients.Include(c => c.PassportData);
}
