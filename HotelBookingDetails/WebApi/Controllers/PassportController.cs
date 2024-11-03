using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassportController(IRepositoryPassport repository, IMapper mapper) : ControllerBase
{   
    /// <summary>
    /// Запрос возвращающий список всех паспортов
    /// </summary>
    /// <returns>список паспортов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Passport), 200)]
    public ActionResult<IEnumerable<Passport>> Get()
    {
        return Ok(repository.GetPassports());
    }

    /// <summary>
    /// Запрос паспорта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса паспорт</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Passport), 200)]
    public ActionResult<Passport> Get(int id)
    {
        var client = repository.GetPassportById(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Запрос на добавления паспорта
    /// </summary>
    /// <param name="passport"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]
    public IActionResult Post([FromBody] PassportDto passport)
    {
        var value = mapper.Map<Passport>(passport);
        repository.PostPassport(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение паспорта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="passport"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] PassportDto passport)
    {
        
        if (repository.GetPassportById(id) == null) { return NotFound("Паспорт с заданным id не найден"); }
        var value = mapper.Map<Passport>(passport);
        repository.PutPassport(id, value);
        return Ok();
    }

    /// <summary>
    /// Удаление паспорта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        repository.DeletePassport(id);
        return Ok();
    }
}
