﻿using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassportController(IRepository<Passport> repository, IMapper mapper) : ControllerBase
{   
    /// <summary>
    /// Запрос возвращающий список всех паспортов
    /// </summary>
    /// <returns>список паспортов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Passport), 200)]
    public ActionResult<IEnumerable<Passport>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Запрос паспорта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса паспорт</returns>
    [HttpGet("{id}")]
    public ActionResult<Passport> Get(int id)
    {
        var client = repository.GetById(id);
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
        repository.Post(value);
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
        if (repository.GetById(id) == null) 
            return NotFound("Паспорт с заданным id не найден"); 
        var value = mapper.Map<Passport>(passport);
        repository.Put(id, value);
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
        if (repository.GetById(id) == null)
            return NotFound("Паспорт с заданным id не найден"); 
        repository.Delete(id);
        return Ok();
    }
}