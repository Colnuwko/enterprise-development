using HotelBookingDetails.Domain.Entity;
using HotelBookingDetails.Shared.Dto;

namespace HotelBookingDetails.WasmClient.Api;

public class ApiClientWrapper(IConfiguration configuration)
{
    private readonly Client _client = new(configuration["OpenApi:ServerUrl"], new HttpClient());
    
    /// <summary>
    /// Вспомогательные клиенты для аналитических запросов
    /// </summary>
    OfClient _ofClient = new(configuration["OpenApi:ServerUrl"], new HttpClient());
    
    AboutClient _aboutClient = new(configuration["OpenApi:ServerUrl"], new HttpClient());
    
    InClient _inClient = new(configuration["OpenApi:ServerUrl"], new HttpClient());
    
    FreeClient _freeClient = new(configuration["OpenApi:ServerUrl"], new HttpClient());
    
    HotelClient _hotelClient = new(configuration["OpenApi:ServerUrl"], new HttpClient());

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
    
    public async Task<ICollection<TypeRoom>> GetTypeRooms()
    {
        return await _client.TypeRoomAllAsync();
    }

    public async Task<TypeRoom> GetTypeRoom(int id)
    {
        return await _client.TypeRoomGETAsync(id);
    }

    public async Task CreateTypeRoom(TypeRoomDto type)
    {
        await _client.TypeRoomPOSTAsync(type);
    }

    public async Task UpdateTypeRoom(int id, TypeRoomDto type)
    {
        await _client.TypeRoomPUTAsync(id, type);
    }

    public async Task DeleteTypeRoom(int id)
    {
        await _client.TypeRoomDELETEAsync(id);
    }
    
    public async Task<ICollection<Passport>> GetPassports()
    {
        return await _client.PassportGETAsync();
    }

    public async Task<Passport> GetPassport(int id)
    {
        return await _client.PassportGET2Async(id);
    }

    public async Task CreatePassport(PassportDto passport)
    {
        await _client.PassportPOSTAsync(passport);
    }

    public async Task UpdatePassport(int id, PassportDto passport)
    {
        await _client.PassportPUTAsync(id, passport);
    }

    public async Task DeletePassport(int id)
    {
        await _client.PassportDELETEAsync(id);
    }
    
    public async Task<ICollection<Domain.Entity.Client>> GetClients()
    {
        return await _client.ClientAllAsync();
    }

    public async Task<Domain.Entity.Client> GetClient(int id)
    {
        return await _client.ClientGETAsync(id);
    }

    public async Task CreateClient(ClientDto client)
    {
        await _client.ClientPOSTAsync(client);
    }

    public async Task UpdateClient(int id, ClientDto client)
    {
        await _client.ClientPUTAsync(id, client);
    }

    public async Task DeleteClient(int id)
    {
        await _client.ClientDELETEAsync(id);
    }
    
    public async Task<ICollection<Room>> GetRooms()
    {
        return await _client.RoomAllAsync();
    }

    public async Task<Room> GetRoom(int id)
    {
        return await _client.RoomGETAsync(id);
    }

    public async Task CreateRoom(RoomDto room)
    {
        await _client.RoomPOSTAsync(room);
    }

    public async Task UpdateRoom(int id, RoomDto room)
    {
        await _client.RoomPUTAsync(id, room);
    }

    public async Task DeleteRoom(int id)
    {
        await _client.RoomDELETEAsync(id);
    }
    
    public async Task<ICollection<ReservedRoom>> GetReservedRooms()
    {
        return await _client.ReservedRoomAllAsync();
    }

    public async Task<ReservedRoom> GetReservedRoom(int id)
    {
        return await _client.ReservedRoomGETAsync(id);
    }

    public async Task CreateReservedRoom(ReservedRoomsDto room)
    {
        await _client.ReservedRoomPOSTAsync(room);
    }

    public async Task UpdateReservedRoom(int id, ReservedRoomsDto room)
    {
        await _client.ReservedRoomPUTAsync(id, room);
    }

    public async Task DeleteReservedRoom(int id)
    {
        await _client.ReservedRoomDELETEAsync(id);
    }

    public async Task<ICollection<HotelsTopFiveDto>> GetTopFiveHotels()
    {
        return await _ofClient.BookingsAsync();
    }

    public async Task<ICollection<HotelsRoomCostDto>> CostInfoAboutHotels()
    {
        return await _aboutClient.HotelsAsync();
    }

    public async Task<ICollection<Domain.Entity.Client>> GetClientsInHotel(int hotelId)
    {
        return await _inClient.HotelAsync(hotelId);
    }

    public async Task<ICollection<Room>> GetFreeRoomsInCity(string city)
    {
        return await _freeClient.RoomsAsync(city);
    }

    public async Task<ICollection<ClientTotalDayDto>> GetLongLiversClient()
    {
        return await _hotelClient.StaysAsync();
    }
}