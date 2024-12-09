using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.WebApi.Dto;

/// <summary>
/// Это кастомный класс для возвращаемого значения аналитического запроса
/// </summary>
/// <remarks>
/// Структура {суммарное время проживания, Клиент}
/// </remarks>
public class ClientTotalDayDto(Client client, int total)
{
    /// <summary>
    /// Клиент
    /// </summary>
    public Client Client { get; set; } = client;

    /// <summary>
    /// суммарное время проживания
    /// </summary>
    public int Total { get; set; } = total;
}
