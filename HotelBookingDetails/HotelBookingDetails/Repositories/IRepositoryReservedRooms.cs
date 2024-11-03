namespace HotelBookingDetails.Domain.Repositories;

public interface IRepositoryReservedRooms
{
    public IEnumerable<ReservedRooms> GetReservedRooms();

    public ReservedRooms? GetReservedRoomById(int id);

    public bool PostReservedRoom(ReservedRooms reservedRooms);

    public bool PutReservedRoom(int id, ReservedRooms reservedRooms);
    public bool DeleteReservedRoom(int id);
}