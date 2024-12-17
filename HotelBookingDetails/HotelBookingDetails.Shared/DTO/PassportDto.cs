namespace HotelBookingDetails.Shared.Dto;

public class PassportDto
{

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Серия паспорта
    /// </summary>
    public required int Series { get; set; }
}