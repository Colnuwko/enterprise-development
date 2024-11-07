namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryClient : IRepository<Client>
{
    private readonly List<Client> _clients = [];
    private int _clientId = 1;

    public bool Put(int id, Client client)
    {
        var oldValue = GetById(id);
        if (oldValue == null)
            return false;
        oldValue.FullName = client.FullName;
        oldValue.PassportData = client.PassportData;
        oldValue.Birthday = client.Birthday;
        return true;
    }

    public bool Delete(int id)
    {
        var client = GetById(id);
        if (client == null) 
            return false; 
        _clients.Remove(client);
        return true;
    }

    public bool Post(Client client)
    {
        client.Id = _clientId++;
        _clients.Add(client);
        return true;
    }

    public Client? GetById(int id) => _clients.Find(c => c.Id == id);

    public IEnumerable<Client> GetAll() => _clients;

}
