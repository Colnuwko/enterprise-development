using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [Column("address")]
    [Required]
    [JsonPropertyName("address")]
    public required string Address { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    [Column("city")]
    [Required]
    [JsonPropertyName("city")]
    public required string City { get; set; }
}