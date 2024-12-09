using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Типы номеров
/// </summary>
[Table("type_room")]
public class TypeRoom
{
    /// <summary>
    /// Идентификатор типа
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    public required int Id { get; set; }

    /// <summary>
    /// Название типа
    /// </summary>
    [Column("name")]
    [Required]
    public required string Name { get; set; }
}