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
    /// <example>yyyy-mm-dd</example>
    public required string DateArriv { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    /// <example>yyyy-mm-dd</example>
    public string? DateDepart { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public required int Period { get; set; }
}
