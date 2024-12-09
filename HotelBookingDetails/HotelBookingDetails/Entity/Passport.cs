using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Column("number")]
    [Required]
    public required int Number { get; set; }

    /// <summary>
    /// Серия паспорта
    /// </summary>
    [Column("series")]
    [Required]
    public required int Series { get; set; }
}
