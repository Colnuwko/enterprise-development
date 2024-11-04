using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeRoomController(IRepositoryTypeRoom repository, IMapper mapper) : ControllerBase
{


    /// <summary>
    /// Запрос возвращающий список типов комнат
    /// </summary>
    /// <returns>список типов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(TypeRoom), 200)]
    public ActionResult<IEnumerable<TypeRoom>> Get()
    {
        return Ok(repository.GetTypeRooms());
    }

    /// <summary>
    /// Запрос типа комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса тип комнаты</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TypeRoom), 200)]
    public ActionResult<TypeRoom> Get(int id)
    {
        var typeRoom = repository.GetTypeRoomById(id);
        if (typeRoom == null) { return NotFound("Тип комнаты с заданным id не найден"); }

        return Ok(typeRoom);
    }

    /// <summary>
    /// Запрос на добавления типа комнаты
    /// </summary>
    /// <param name="typeRoom"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]
    public IActionResult Post([FromBody] TypeRoomDto typeRoom)
    {
        var value = mapper.Map<TypeRoom>(typeRoom);

        repository.PostTypeRoom(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение типа комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="typeRoom"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TypeRoomDto typeRoom)
    {

        if (repository.GetTypeRoomById(id) == null) { return NotFound("Тип комнаты с заданным id не найден"); }
        var value = mapper.Map<TypeRoom>(typeRoom);
        repository.PutTypeRoom(id, value);
        return Ok();
    }

    /// <summary>
    /// Удаление типа комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.GetTypeRoomById(id) == null) { return NotFound("Тип комнаты с заданным id не найден"); }
        repository.DeleteTypeRoom(id);
        return Ok();
    }
}
