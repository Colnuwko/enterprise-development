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
    /// День рождения
    /// </summary>
    public required DateOnly Birthday { get; set; }
}
