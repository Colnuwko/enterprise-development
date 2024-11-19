using HotelBookingDetails.Domain;

namespace HotelBookingDetails.WebApi.Dto;

/// <summary>
/// Это кастомный класс для возвращаемого значения аналитического запроса
/// </summary>
/// <remarks>
/// Структура {Отель, количество бронирований}
/// </remarks>
public class HotelsTopFiveDto(Hotel hotel, int countOfBookings)
{
    /// <summary>
    /// Отель
    /// </summary>
    public Hotel Hotel { get; set; } = hotel;

    /// <summary>
    /// Количество бронирований
    /// </summary>
    public int CountOfBookings { get; set; } = countOfBookings;
}
