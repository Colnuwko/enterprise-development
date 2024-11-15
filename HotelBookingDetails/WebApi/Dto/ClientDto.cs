using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class ClientDto
{    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// id Пасспортныx данных
    /// </summary>
    public required int PassportDataId { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    [Range(1900, 2024, ErrorMessage = "Недопустимый год рождения. Допустимые значения (1900 - 2024)")]
    public required int BirthdayYear { get; set; }

    /// <summary>
    /// Месяц рождения
    /// </summary>
    [Range(1, 12, ErrorMessage = "Недопустимый месяц рождения. Допустимы значения (1 - 12)")]
    public required int BirthdayMonth { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    [Range(1, 31, ErrorMessage = "Недопустимый день рождения. Допустимые значения (1 - 31")]
    public required int BirthdayDay { get; set; }
}