namespace HotelBookingDetails.Domain.Repositories;

internal interface IReposirotyHotel
{
    public IEnumerable<Hotel> GetHotels();

    public Hotel? GetHotelById(int id);

    public bool PostHotel(Hotel hotel);

    public bool PutHotel(int id, Hotel hotel);
    public bool DeleteHotel(int id);
}