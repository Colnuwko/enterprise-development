using System;
using System.Collections.Generic;
using System.Linq;
namespace HotelBookingDetails.Domain.Repositories;
public interface IRepository<C>
{
    public IEnumerable<C> GetAll();

    public C? GetById(int id);

    public bool Post(C client);

    public bool Put(int id, C client);

    public bool Delete(int id);
}