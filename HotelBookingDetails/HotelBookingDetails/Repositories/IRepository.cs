using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingDetails.Domain.Repositories;

public interface IRepository
{
    /// <summary>
    /// nsfjinsdf
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Client> GetClients();

    public Client? GetClientById(int id);

    public bool PostClient(Client client);

    public bool PutClient(int id, Client client);
    public bool DeleteClient(int id);


    public IEnumerable<Hotel> GetHotels();

    public Hotel? GetHotelById(int id);

    public bool PostHotel(Hotel client);

    public bool PutHotel(int id, Hotel client);
    public bool DeleteHotel(int id);


    public IEnumerable<Passport> GetPassports();

    public Passport? GetPassportById(int id);

    public bool PostPassport(Passport client);

    public bool PutPassport(int id, Passport client);
    public bool DeletePassport(int id);


    public IEnumerable<ReservedRooms> GetReservedRooms();

    public ReservedRooms? GetReservedRoomsById(int id);

    public bool PostReservedRooms(ReservedRooms client);

    public bool PutReservedRooms(int id, ReservedRooms client);
    public bool DeleteReservedRooms(int id);


    public IEnumerable<Room> GetRooms();

    public Room? GetRoomById(int id);

    public bool PostRoom(Room client);

    public bool PutRoom(int id, Room client);
    public bool DeleteRoom(int id);

    public IEnumerable<TypeRoom> GetTypeRooms();

    public TypeRoom? GetTypeRoomById(int id);

    public bool PostTypeRoom(TypeRoom client);

    public bool PutTypeRoom(int id, TypeRoom client);
    public bool DeleteTypeRoom(int id);
}
