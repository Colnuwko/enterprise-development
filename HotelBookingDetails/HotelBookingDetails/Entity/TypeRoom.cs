using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название типа
    /// </summary>
    [Column("name")]
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}