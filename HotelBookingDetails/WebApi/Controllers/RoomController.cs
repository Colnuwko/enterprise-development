﻿using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain.Repositories;
using AutoMapper;
using HotelBookingDetails.Shared.Dto;
using HotelBookingDetails.Domain.Entity;
namespace HotelBookingDetails.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IRepository<Room> repositoryRoom, IRepository<TypeRoom> repositoryType, IRepository<Hotel> repositoryHotel, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий список всех комнат
    /// </summary>
    /// <returns>список комнат</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Room>> Get()
    {
        return Ok(repositoryRoom.GetAll());
    }

    /// <summary>
    /// Запрос комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Объект класса комната</returns>
    [HttpGet("{id}")]
    public ActionResult<Room> Get(int id)
    {
        var room = repositoryRoom.GetById(id);
        if (room == null)
            return NotFound("Комната с заданным id не найден");
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
        var hotel = repositoryHotel.GetById(room.HotelId);
        if (hotel == null)
            return NotFound("Не найден отель по заданному id");
        var type = repositoryType.GetById(room.TypeId);
        if (type == null)
            return NotFound("Не найден тип комнаты по заданному id");
        var value = mapper.Map<Room>(room);
        value.Hotel = hotel;
        value.Type = type;
        repositoryRoom.Post(value);
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
        var hotel = repositoryHotel.GetById(room.HotelId);
        if (hotel == null)
            return NotFound("Не найден отель по заданному id");
        var type = repositoryType.GetById(room.TypeId);
        if (type == null)
            return NotFound("Не найден тип комнаты по заданному id");
        var value = mapper.Map<Room>(room);
        value.Hotel = hotel;
        value.Type = type;
        if (repositoryRoom.Put(id, value))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Удаление комнаты по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repositoryRoom.Delete(id))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }
}