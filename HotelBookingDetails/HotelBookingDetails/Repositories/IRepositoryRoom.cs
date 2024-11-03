namespace HotelBookingDetails.Domain.Repositories;

public interface IRepositoryRoom
{
    public IEnumerable<Room> GetRooms();

    public Room? GetRoomById(int id);

    public bool PostRoom(Room room);

    public bool PutRoom(int id, Room room);
    public bool DeleteRoom(int id);

}