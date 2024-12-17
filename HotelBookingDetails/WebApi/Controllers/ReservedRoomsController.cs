using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain.Repositories;
using AutoMapper;
using HotelBookingDetails.Shared.Dto;
using HotelBookingDetails.Domain.Entity;
using System.Linq;
namespace HotelBookingDetails.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservedRoomController(IRepository<ReservedRooms> repositoryReservedRooms, IRepository<Room> repositoryRoom, IRepository<Client> repositoryClient,
        IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос возвращающий полную информацию обо всех бронированиях
    /// </summary>
    /// <returns>список бронирований</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ReservedRooms>> Get()
    {
        return Ok(repositoryReservedRooms.GetAll());
    }

    /// <summary>
    /// Запрос информации о обронировании по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Полную информацию о бронировании</returns>
    [HttpGet("{id}")]
    public ActionResult<ReservedRooms> Get(int id)
    {
        var reservedRoom = repositoryReservedRooms.GetById(id);
        if (reservedRoom == null)
            return NotFound();
        return Ok(reservedRoom);
    }

    /// <summary>
    /// Запрос на добавления бронирования
    /// </summary>
    /// <param name="reservedRoom"></param>
    /// <returns>Код выполнения</returns>
    [HttpPost]
    public IActionResult Post([FromBody] ReservedRoomsDto reservedRoom)
    {
        var value = mapper.Map<ReservedRooms>(reservedRoom);
        var client = repositoryClient.GetById(reservedRoom.ClientId);
        var room = repositoryRoom.GetById(reservedRoom.RoomId);
        if (client == null)
            return NotFound("Клиент не найден по заданному id");
        value.Client = client;
        if (room == null)
            return NotFound("Комната по заданному id не найдена");
        value.Room = room;
        repositoryReservedRooms.Post(value);
        return Ok();
    }

    /// <summary>
    /// Запрос на изменение бронирования по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reservedRoom"></param>
    /// <returns>Код выполнения</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ReservedRoomsDto reservedRoom)
    {
        var value = mapper.Map<ReservedRooms>(reservedRoom);
        var client = repositoryClient.GetById(reservedRoom.ClientId);
        var room = repositoryRoom.GetById(reservedRoom.RoomId);
        if (client == null)
            return NotFound("Клиент не найден по заданному id");
        value.Client = client;
        if (room == null)
            return NotFound("Комната по заданному id не найдена");
        value.Room = room;
        if (repositoryReservedRooms.Put(id, value))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Удаление бронирования по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repositoryReservedRooms.Delete(id))
            return Ok();
        return NotFound("Объект по заданному id не найден");
    }

    /// <summary>
    /// Возвращает всех клиентов отеля
    /// </summary>
    /// <param name="hotelId"></param>
    /// <returns>Список клиентов</returns>
    [HttpGet("get_all_client_in_hotel/{hotelId}")]
    public ActionResult<IEnumerable<Client>> GetAllClientInHotel(int hotelId)
    {
        var result = (from reservedRoom in repositoryReservedRooms.GetAll()
                        where reservedRoom.Room.Hotel.Id == hotelId
                        select reservedRoom.Client)
                        .OrderBy(c => c.FullName).Distinct();
        return Ok(result);
    }

    /// <summary>
    /// Возвращает все свободные комнаты в выбранном городе
    /// </summary>
    /// <param name="city"></param>
    /// <returns>Список свободных комнат</returns>
    [HttpGet("get_all_free_rooms/{city}")]
    public ActionResult<IEnumerable<Room>> GetFreeRoomInCity(string city)
    {
        var reservedRooms = repositoryReservedRooms.GetAll();

        if (!reservedRooms.Any())
        {
            var rooms = from room in repositoryRoom.GetAll()
                        where room.Hotel.City == city
                        select room;

            return Ok(rooms);
        }

        var result = from reservedRoom in reservedRooms
                     where reservedRoom.Room.Hotel.City == city && reservedRoom.DateDeparture != null
                     select reservedRoom.Room;
        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиентов с наибольшим временем проживания в отелях
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet("get_clients_with_the_longest_hotel_stays")]
    public ActionResult<IEnumerable<ClientTotalDayDto>> GetLongLiversClient()
    {
        var reservedRoom = repositoryReservedRooms.GetAll();
        if (!reservedRoom.Any())
            return NotFound("Информация о бронировании отсутствует. Пожалуйста, добавьте новые записи о бронировании");
        var longerPeriods = reservedRoom
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Max(c => c.Total);

        var clientWithLongerPer = reservedRoom
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Where(c => c.Total == longerPeriods).Select(c => new ClientTotalDayDto(c.Client, c.Total)).ToList();
        return Ok(clientWithLongerPer);
    }
}