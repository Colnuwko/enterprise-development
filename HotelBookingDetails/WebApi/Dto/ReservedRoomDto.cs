namespace WebApi.Dto;

public class ReservedRoomsDto
{
    /// <summary>
    /// Клиент  
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Комната
    /// </summary
    public required int RoomId { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    public required DateOnly DateArrival { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    public DateOnly DateDeparture { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public required int Period { get; set; }
}
