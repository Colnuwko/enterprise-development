using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepository<Client> repository, IRepository<Passport> repositoryPassport, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список всех клиентов
    /// </summary>
    /// <returns>список клиентов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Запрос клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса клиент</returns>
    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        var client = repository.GetById(id);
        if (client == null)
            return NotFound();
        return Ok(client);
    }

    /// <summary>
    /// Запрос на добавления клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]

    public IActionResult Post([FromBody] ClientDto client)
    {
        if (client.PassportDataId == null)
            return NotFound("Не найдены паспортные данные по заданному id");
        var value = mapper.Map<Client>(client);
        value.PassportData =  repositoryPassport.GetById(client.PassportDataId);
        repository.Post(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientDto client)
    {
        if (repository.GetById(id) == null) 
            return NotFound("Клиент с заданным id не найден"); 
        if (repositoryPassport.GetById(client.PassportDataId) == null) 
            return NotFound("Не найдены паспортные данные по заданному id"); 
        var value = mapper.Map<Client>(client);
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
            return NotFound("Клиент с заданным id не найден"); 
        repository.Delete(id);
        return Ok();
    }
}