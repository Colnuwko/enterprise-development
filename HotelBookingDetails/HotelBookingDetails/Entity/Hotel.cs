using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Отель
/// </summary>
[Table("hotel")]
public class Hotel
{
    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    public required int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [Column("address")]
    [Required]
    public required string Address { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    [Column("city")]
    [Required]
    public required string City { get; set; }
}