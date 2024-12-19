using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    [Column("type_id")]
    [Required]
    [JsonPropertyName("type")]
    public required TypeRoom Type { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    [Column("capacity")]
    [Required]
    [JsonPropertyName("capacity")]
    public required int Capacity { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [Column("cost")]
    [Required]
    [JsonPropertyName("cost")]
    public required int Cost { get; set; }

    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    [Column("hotel_id")]
    [Required]
    [JsonPropertyName("hotel")]
    public required Hotel Hotel { get; set; }
    
    [JsonIgnore]
    public string TypeName => Type.Name;
    
    [JsonIgnore]
    public string HotelName => Hotel.Name;
}