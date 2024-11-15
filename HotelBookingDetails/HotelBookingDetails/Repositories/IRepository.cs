namespace HotelBookingDetails.Domain.Repositories;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();

    public T? GetById(int id);

    public void Post(T client);

    public bool Put(int id, T client);

    public bool Delete(int id);
}