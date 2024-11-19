﻿using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using AutoMapper;
using HotelBookingDetails.WebApi.Dto;
namespace HotelBookingDetails.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepository<Client> repositoryClient, IRepository<Passport> repositoryPassport, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список всех клиентов
    /// </summary>
    /// <returns>список клиентов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repositoryClient.GetAll());
    }

    /// <summary>
    /// Запрос клиента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса клиент</returns>
    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        var client = repositoryClient.GetById(id);
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
        if (repositoryPassport.GetById(client.PassportDataId) == null)
            return NotFound("Не найдены паспортные данные по заданному id");
        var value = mapper.Map<Client>(client);
        value.PassportData = repositoryPassport.GetById(client.PassportDataId)!;
        repositoryClient.Post(value);
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
        if (repositoryPassport.GetById(client.PassportDataId) == null)
            return NotFound("Не найдены паспортные данные по заданному id");
        var value = mapper.Map<Client>(client);
        if (repositoryClient.Put(id, value))
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
        if (repositoryClient.Delete(id))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }
}