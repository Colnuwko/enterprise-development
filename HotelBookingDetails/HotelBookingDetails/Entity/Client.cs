    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBookingDetails.Domain.Entity;

/// <summary>
/// Клиент
/// </summary>
[Table("client")]
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Key]
    [Column("id")]
    [Required]
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("full_name")]
    [Required]
    [JsonPropertyName("full_name")]
    public required string FullName { get; set; }

    /// <summary>
    /// Пасспортные данные
    /// </summary>
    [Column("passport_id")]
    [Required]
    [JsonPropertyName("passport_data")]
    public required Passport PassportData { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    [Column("birthday")]
    [Required]  
    [JsonPropertyName("birthday")]
    public required DateOnly? Birthday { get; set; }

    [JsonIgnore] public string PassportSeriesNumber => $"{PassportData.Series} {PassportData.Number}";
}