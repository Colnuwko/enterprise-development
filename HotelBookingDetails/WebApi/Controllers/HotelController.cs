using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using AutoMapper;
using HotelBookingDetails.WebApi.Dto;
namespace HotelBookingDetails.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IRepository<Hotel> repository, IRepository<ReservedRooms> repositoryReserved, IRepository<Room> repositoryRoom, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список всех отелей
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Запрос возвращающий количество отелей
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    public ActionResult<int> GetCount()
    {
        return Ok(repository.GetAll().Count());
    }

    /// <summary>
    /// Запрос отеля по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса отель</returns>
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = repository.GetById(id);
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
        repository.Post(value);
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
        if (repository.Put(id, value))
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
        if (repository.Delete(id))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Запрос возвращающий топ 5 отелей по количеству бронирований
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet("top_5_hotels_by_number_of_bookings")]
    public ActionResult<IEnumerable<Hotel>> GetTopFiveHotels()
    {
        var topFiveHotelId = repositoryReserved.GetAll().GroupBy(r => r.Room.HotelId).Select(r => r.Key).Take(5);
        return Ok(repository.GetAll().Where(h => topFiveHotelId.Contains(h.Id)));
    }

    /// <summary>
    /// Запрос возвращающий минимальную максимальную и среднюю цену комнат для каждого отеля
    /// </summary>
    /// <returns>структура {отель, минимальная цена комнаты, максимальная цена, средняя цена}</returns>
    [HttpGet("cost_info_about_hotels")]
    public ActionResult<IEnumerable<ReturnTypeHotel>> GetTop()
    {
        var rooms = repositoryRoom.GetAll();
        var hotelCosts = repository.GetAll().Select(h => new ReturnTypeHotel
        (
            repository.GetAll().Where(hotel => hotel.Id == h.Id).Select(hotel => hotel),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Min(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Max(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Average(rm => rm.Cost)
        )).AsEnumerable();
        return Ok(hotelCosts);
    }
}