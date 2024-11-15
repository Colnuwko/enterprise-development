namespace HotelBookingDetails.WebApi.Dto;

public class RoomDto
{
    /// <summary>
    /// Тип номера
    /// </summary>
    public required int TypeId { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    public required int Capacity { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public required int Cost { get; set; }

    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    public required int HotelId { get; set; }
}