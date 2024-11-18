using HotelBookingDetails.Domain;

namespace HotelBookingDetails.WebApi.Dto;

public class ReservedRoomsDto
{
    /// <summary>
    /// Клиент  
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Комната
    /// </summary>
    public required int RoomId { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    /// <example>yyyy-mm-dd</example>
    public required string DateArrival { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    /// <example>yyyy-mm-dd</example>
    public string? DateDeparture { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public required int Period { get; set; }
}
