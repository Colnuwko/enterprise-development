    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public required int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("full_name")]
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Пасспортные данные
    /// </summary>
    [Column("passport_id")]
    [Required]
    public required Passport PassportData { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    [Column("birthday")]
    [Required]  
    public required DateOnly? Birthday { get; set; }
}