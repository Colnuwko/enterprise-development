using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepositoryClient repository, IRepositoryPassport repositoryPassport, IMapper mapper) : ControllerBase
{
    
    
    /// <summary>
    /// Запрос возвращающий список всех клиентов
    /// </summary>
    /// <returns>список клиентов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repository.GetClients());
    }

    /// <summary>
    /// Запрос клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса клиент</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), 200)]
    public ActionResult<Client> Get(int id)
    {
        var client = repository.GetClientById(id);
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
        var value = mapper.Map<Client>(client);
        value.Birthday = new DateOnly(client.BirthdayYear, client.BirthdayMonth, client.BirthdayDay);
        value.PassportData =  repositoryPassport.GetPassportById(client.PassportDataId);
        if (value.PassportData == null) { return NotFound("Не найдены паспортные данные по заданому id"); }
        repository.PostClient(value);
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
        
        if (repository.GetClientById(id) == null) { return NotFound("Клиент с заданным id не найден"); }
        if (repositoryPassport.GetPassportById(client.PassportDataId) == null) { return NotFound("Не найдены паспортные данные по заданому id"); }
        var value = mapper.Map<Client>(client);
        repository.PutClient(id, value);
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
        if (repository.GetClientById(id) == null) { return NotFound("Клиент с заданным id не найден"); }
        repository.DeleteClient(id);
        return Ok();
    }
}
