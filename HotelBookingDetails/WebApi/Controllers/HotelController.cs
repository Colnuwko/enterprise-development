using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IRepositoryHotel repository, IRepositoryReservedRooms repositoryReserved, IRepositoryRoom repositoryRoom, IMapper mapper) : ControllerBase
{   
    /// <summary>
    /// Запрос возвращающий список всех отелей
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(repository.GetHotels());
    }

    /// <summary>
    /// Запрос возвращающий количество отелей
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    public ActionResult<int> GetCount()
    {
        return Ok(repository.GetCountHotels());
    }

    /// <summary>
    /// Запрос отеля по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса отель</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Hotel), 200)]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = repository.GetHotelById(id);
        if (hotel == null) { return NotFound("Отель с заданным id не найден"); }

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
        
        repository.PostHotel(value);
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

        if (repository.GetHotelById(id) == null) { return NotFound("Отель с заданным id не найден"); }
        var value = mapper.Map<Hotel>(hotel);
        repository.PutHotel(id, value);
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
        if (repository.GetHotelById(id) == null) { return NotFound("Отель с заданным id не найден"); }
        repository.DeleteHotel(id);
        return Ok();
    }

    /// <summary>
    /// Запрос возвращающий топ 5 отелей
    /// </summary>
    /// <returns>список отелей</returns>
    [HttpGet("top_five_hotels")]
    public ActionResult<IEnumerable<Hotel>> GetTopFiveHotels()
    {
        return Ok(repository.GetTopFiveHotelById(repositoryReserved.GetTopFiveHotelId()));
    }


    /// <summary>
    /// Запрос возвращающий минимальную максимальную и среднюю цену комнат для каждого отеля
    /// </summary>
    /// <returns>структура {отель, минимальная цена комнаты, максимальная цена, средняя цена}</returns>
    [HttpGet("cost_info_about_hotels")]
    public ActionResult<IEnumerable<T>> GetTop()
    {

        return Ok(repository.GetMaxAvgMinForHotels(repositoryRoom.GetRooms()));
    }
    
}
