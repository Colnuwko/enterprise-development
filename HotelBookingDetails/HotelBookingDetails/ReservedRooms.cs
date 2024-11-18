namespace HotelBookingDetails.Domain;

/// <summary>
/// Забронированные номера
/// </summary>
public class ReservedRooms
{
    /// <summary>
    /// Идентификатор зарезервированных комнат
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент  
    /// </summary>
    public required Client Client {  get; set; }

    /// <summary>
    /// Комната
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    public required DateOnly DateArrival { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    public DateOnly? DateDeparture { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public required int Period { get; set; }
}