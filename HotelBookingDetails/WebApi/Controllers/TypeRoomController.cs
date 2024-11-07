using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeRoomController(IRepository<TypeRoom> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список типов комнат
    /// </summary>
    /// <returns>список типов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TypeRoom>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Запрос типа комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса тип комнаты</returns>
    [HttpGet("{id}")]
    public ActionResult<TypeRoom> Get(int id)
    {
        var typeRoom = repository.GetById(id);
        if (typeRoom == null)  
            return NotFound("Тип комнаты с заданным id не найден"); 
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
        repository.Post(value);
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
        if (repository.GetById(id) == null) 
            return NotFound("Тип комнаты с заданным id не найден"); 
        var value = mapper.Map<TypeRoom>(typeRoom);
        repository.Put(id, value);
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
        if (repository.GetById(id) == null) 
            return NotFound("Тип комнаты с заданным id не найден"); 
        repository.Delete(id);
        return Ok();
    }
}