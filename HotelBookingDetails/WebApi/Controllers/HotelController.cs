using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
namespace WebApi.Controllers;

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
        return Ok(GetCountHotels());
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
        if (repository.GetById(id) == null) 
            return NotFound("Отель с заданным id не найден"); 
        var value = mapper.Map<Hotel>(hotel);
        repository.Put(id, value);
        return Ok();
    }

    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.GetById(id) == null) 
            return NotFound("Отель с заданным id не найден"); 
        repository.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Запрос возвращающий топ 5 отелей по количеству бронирований
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet("top_5_hotels_by_number_of_bookings")]
    public ActionResult<IEnumerable<Hotel>> GetTopFiveHotels()
    {
        return Ok(GetTopFiveHotelById(GetTopFiveHotelId()));
    }

    /// <summary>
    /// Запрос возвращающий минимальную максимальную и среднюю цену комнат для каждого отеля
    /// </summary>
    /// <returns>структура {отель, минимальная цена комнаты, максимальная цена, средняя цена}</returns>
    [HttpGet("cost_info_about_hotels")]
    public ActionResult<IEnumerable<T>> GetTop()
    {
        return Ok(GetMaxAvgMinForHotels(repositoryRoom.GetAll()));
    }

    [NonAction]
    public int GetCountHotels() => repository.GetAll().Count();

    [NonAction]
    public IEnumerable<Hotel> GetTopFiveHotelById(IEnumerable<int> id)
    {
        return repository.GetAll().Where(h => id.Contains(h.Id));
    }

    [NonAction]
    public IEnumerable<T> GetMaxAvgMinForHotels(IEnumerable<Room> rooms)
    {

        var hotelCosts = repository.GetAll().Select(h => new T
        (
            (repository.GetAll().Where(hotel => hotel.Id == h.Id).Select(hotel => hotel)),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Min(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Max(rm => rm.Cost),
            rooms.Where(r => r.HotelId == h.Id).Select(r => r).ToList().Average(rm => rm.Cost)
        )).AsEnumerable();

        return hotelCosts;
    }

    [NonAction]
    public IEnumerable<int> GetTopFiveHotelId() => repositoryReserved.GetAll().GroupBy(r => r.Room.HotelId).Select(r => r.Key).Take(5);
}