using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Паспорт
/// </summary>
[Table("passport")]
public class Passport
{
    /// <summary>
    /// Идентификатор паспорта
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Column("number")]
    [Required]
    [JsonPropertyName("number")]
    public required int Number { get; set; }

    /// <summary>
    /// Серия паспорта
    /// </summary>
    [Column("series")]
    [Required]
    [JsonPropertyName("series")]
    public required int Series { get; set; }
}
