using System.Text.Json.Serialization;
using HotelBookingDetails.Domain.Entity;

namespace HotelBookingDetails.Shared.Dto;

/// <summary>
/// Это кастомный класс для возвращаемого значения аналитического запроса
/// </summary>
/// <remarks>
/// Структура {суммарное время проживания, Клиент}
/// </remarks>
public class ClientTotalDayDto(Client curClient, int total)
{
    /// <summary>
    /// Клиент
    /// </summary>
    [JsonPropertyName("curClient")]
    public Client CurClient { get; set; } = curClient;

    /// <summary>
    /// суммарное время проживания
    /// </summary>
    public int Total { get; set; } = total;
}
