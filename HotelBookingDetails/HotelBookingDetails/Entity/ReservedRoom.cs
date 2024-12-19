using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Забронированные номера
/// </summary>
[Table("reserved_room")]
public class ReservedRoom
{
    /// <summary>
    /// Идентификатор зарезервированных комнат
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Клиент  
    /// </summary>
    [Column("client_id")]
    [Required]
    [JsonPropertyName("client")]
    public required Client Client { get; set; }

    /// <summary>
    /// Комната
    /// </summary>
    [Column("room_id")]
    [Required]
    [JsonPropertyName("room")]
    public required Room Room { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    [Column("date_arrival")]
    [Required]
    [JsonPropertyName("date_arrival")]
    public required DateOnly DateArrival { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    [Column("date_departure")]
    [JsonPropertyName("date_departure")]
    public DateOnly? DateDeparture { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    [Column("period")]
    [Required]
    [JsonPropertyName("period")]
    public required int Period { get; set; }

    [JsonIgnore] public string ClientName => Client.FullName;

    [JsonIgnore] public int RoomNumber => Room.Id;
}