using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IRepositoryRoom repository, IRepositoryTypeRoom repositoryType, IMapper mapper) : ControllerBase
{


    /// <summary>
    /// Запрос возвращающий список всех комнат
    /// </summary>
    /// <returns>список комнат</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Room>> Get()
    {
        return Ok(repository.GetRooms());
    }

    /// <summary>
    /// Запрос комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса комната</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Room), 200)]
    public ActionResult<Room> Get(int id)
    {
        var room = repository.GetRoomById(id);
        if (room == null) { return NotFound("Комната с заданным id не найден"); }

        return Ok(room);
    }

    /// <summary>
    /// Запрос на добавления комнаты
    /// </summary>
    /// <param name="room"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]
    public IActionResult Post([FromBody] RoomDto room)
    {
        var value = mapper.Map<Room>(room);
        
        repository.PostRoom(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="room"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] RoomDto room)
    {

        if (repository.GetRoomById(id) == null) { return NotFound("Комната с заданным id не найден"); }
        if (repositoryType.GetTypeRoomById(room.TypeId) == null) { return NotFound("Комната с заданным Typeid не найден(тип комнаты неверен)"); }
        var value = mapper.Map<Room>(room);
        repository.PutRoom(id, value);
        return Ok();
    }

    /// <summary>
    /// Удаление комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.GetRoomById(id) == null) { return NotFound("Комната с заданным id не найден"); }
        repository.DeleteRoom(id);
        return Ok();
    }

}
