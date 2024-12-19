using System.Text.Json.Serialization;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Shared.Dto;

/// <summary>
/// Это кастомный класс для возвращаемого значения аналитического запроса
/// </summary>
/// <remarks>
/// Структура {Отель, количество бронирований}
/// </remarks>
public class HotelsTopFiveDto(Hotel curHotel, int countOfBookings)
{
    /// <summary>
    /// Отель
    /// </summary>
    [JsonPropertyName("curHotel")]
    public Hotel CurHotel { get; set; } = curHotel;


    /// <summary>
    /// Количество бронирований
    /// </summary>
    public int CountOfBookings { get; set; } = countOfBookings;
}
