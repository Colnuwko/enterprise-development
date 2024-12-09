using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Комната
/// </summary>
[Table("room")]
public class Room
{
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    public required int Id { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    [Column("type_id")]
    [Required]
    public required TypeRoom Type { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    [Column("capacity")]
    [Required]
    public required int Capacity { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [Column("cost")]
    [Required]
    public required int Cost { get; set; }

    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    [Column("hotel_id")]
    [Required]
    public required int HotelId { get; set; }
}