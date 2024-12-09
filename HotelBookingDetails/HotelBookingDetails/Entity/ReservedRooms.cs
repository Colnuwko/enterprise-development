using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Забронированные номера
/// </summary>
[Table("reserved_room")]
public class ReservedRooms
{
    /// <summary>
    /// Идентификатор зарезервированных комнат
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    public required int Id { get; set; }

    /// <summary>
    /// Клиент  
    /// </summary>
    [Column("client_id")]
    [Required]
    public required Client Client { get; set; }

    /// <summary>
    /// Комната
    /// </summary>
    [Column("room_id")]
    [Required]
    public required Room Room { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    [Column("date_arrival")]
    [Required]
    public required DateOnly DateArrival { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    [Column("date_departure")]
    public DateOnly? DateDeparture { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    [Column("period")]
    [Required]
    public required int Period { get; set; }
}