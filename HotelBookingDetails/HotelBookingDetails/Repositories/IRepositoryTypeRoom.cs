namespace HotelBookingDetails.Domain.Repositories;

internal interface IRepositoryTypeRoom
{
    public IEnumerable<TypeRoom> GetTypeRooms();

    public TypeRoom? GetTypeRoomById(int id);

    public bool PostTypeRoom(TypeRoom typeRoom);

    public bool PutTypeRoom(int id, TypeRoom typeRoom);
    public bool DeleteTypeRoom(int id);
}