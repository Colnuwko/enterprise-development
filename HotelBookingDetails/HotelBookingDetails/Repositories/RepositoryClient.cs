
namespace HotelBookingDetails.Domain.Repositories;

public class RepositoryClient : IRepositoryClient
{
    private readonly List<Client> _clients = [];
    private int _clientId = 1;

    public bool PutClient(int id, Client client)
    {
        var oldValue = GetClientById(id);
        oldValue.FullName = client.FullName;
        oldValue.PassportData = client.PassportData;
        oldValue.Birthday = client.Birthday;
        return true;
    }

    public bool DeleteClient(int id)
    {
        var client = GetClientById(id);
        if (client == null) { return false; }
        _clients.Remove(client);
        return true;
    }

    public bool PostClient(Client client)
    {
        client.Id = _clientId++;
        _clients.Add(client);
        return true;
    }

    public Client? GetClientById(int id) => _clients.Find(c => c.Id == id);

    public IEnumerable<Client> GetClients() => _clients;

}
