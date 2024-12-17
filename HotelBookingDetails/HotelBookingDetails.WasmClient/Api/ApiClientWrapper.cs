using HotelBookingDetails.Domain.Entity;
using HotelBookingDetails.Shared.Dto;

namespace HotelBookingDetails.WasmClient.Api;

public class ApiClientWrapper(IConfiguration configuration)
{
    private readonly Client _client = new(configuration["OpenApi:ServerUrl"], new HttpClient());

    public async Task<ICollection<Hotel>> GetHotels()
    {
        return await _client.HotelAllAsync();
    }

    public async Task<Hotel> GetHotel(int id)
    {
        return await _client.HotelGETAsync(id);
    }

    public async Task CreateHotel(HotelDto hotel)
    {
        await _client.HotelPOSTAsync(hotel);
    }

    public async Task UpdateHotel(int id, HotelDto hotel)
    {
        await _client.HotelPUTAsync(id, hotel);
    }

    public async Task DeleteHotel(int id)
    {
        await _client.HotelDELETEAsync(id);
    }
}