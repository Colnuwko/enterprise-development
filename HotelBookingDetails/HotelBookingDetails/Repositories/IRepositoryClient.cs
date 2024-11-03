using System;
using System.Collections.Generic;
using System.Linq;
namespace HotelBookingDetails.Domain.Repositories;
public interface IRepositoryClient
{
    public IEnumerable<Client> GetClients();

    public Client? GetClientById(int id);

    public bool PostClient(Client client);

    public bool PutClient(int id, Client client);
    public bool DeleteClient(int id);
}