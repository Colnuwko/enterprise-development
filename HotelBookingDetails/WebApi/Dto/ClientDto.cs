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
    public required int BirthdayYear { get; set; }

    /// <summary>
    /// Месяц рождения
    /// </summary>
    public required int BirthdayMonth { get; set; }

    /// <summary>
    /// День рождения
    /// </summary>
    public required int BirthdayDay { get; set; }

}
