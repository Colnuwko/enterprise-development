using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain.Repositories;
using AutoMapper;
using HotelBookingDetails.WebApi.Dto;
using HotelBookingDetails.Domain.Entity;
namespace HotelBookingDetails.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IRepository<Hotel> repositoryHotel, IRepository<ReservedRooms> repositoryReserved, IRepository<Room> repositoryRoom, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список всех отелей
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(repositoryHotel.GetAll());
    }

    /// <summary>
    /// Запрос возвращающий количество отелей
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    public ActionResult<int> GetCount()
    {
        return Ok(repositoryHotel.GetAll().Count());
    }

    /// <summary>
    /// Запрос отеля по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса отель</returns>
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = repositoryHotel.GetById(id);
        if (hotel == null)
            return NotFound("Отель с заданным id не найден");
        return Ok(hotel);
    }

    /// <summary>
    /// Запрос на добавления отеля
    /// </summary>
    /// <param name="hotel"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]
    public IActionResult Post([FromBody] HotelDto hotel)
    {
        var value = mapper.Map<Hotel>(hotel);
        repositoryHotel.Post(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="hotel"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] HotelDto hotel)
    {
        var value = mapper.Map<Hotel>(hotel);
        if (repositoryHotel.Put(id, value))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repositoryHotel.Delete(id))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Запрос возвращающий топ 5 отелей по количеству бронирований
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet("top_5_hotels_by_number_of_bookings")]
    public ActionResult<IEnumerable<HotelsTopFiveDto>> GetTopFiveHotels()
    {
        var result = (from reserverdRooms in repositoryReserved.GetAll()
                      join hotel in repositoryHotel.GetAll() on reserverdRooms.Room.HotelId equals hotel.Id
                      group repositoryRoom by hotel into g
                      select new HotelsTopFiveDto(g.Key, g.Count()))
                     .OrderBy(h => h.CountOfBookings)
                     .Take(5);
        return Ok(result);
    }

    /// <summary>
    /// Запрос возвращающий минимальную максимальную и среднюю цену комнат для каждого отеля
    /// </summary>
    /// <returns>структура {отель, минимальная цена комнаты, максимальная цена, средняя цена}</returns>
    [HttpGet("cost_info_about_hotels")]
    public ActionResult<IEnumerable<HotelsRoomCostDto>> GetTop()
    {
        var rooms = repositoryRoom.GetAll();
        var hotelCosts = repositoryHotel.GetAll().Select(h => new HotelsRoomCostDto
        (
            h,
            rooms.Where(r => r.HotelId == h.Id).Min(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Max(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Average(rm => rm.Cost)
        ));
        return Ok(hotelCosts);
    }
}