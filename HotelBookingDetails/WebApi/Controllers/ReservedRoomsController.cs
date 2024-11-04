using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservedRoomController(IRepositoryReservedRooms repository, IRepositoryRoom repositoryRoom, IRepositoryClient repositoryClient,
    IRepositoryHotel repositoryHotel, IMapper mapper) : ControllerBase
{


    /// <summary>
    /// Запрос возвращающий полную информацию обо всех бронированиях
    /// </summary>
    /// <returns>список бронирований</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ReservedRooms>> Get()
    {
        return Ok(repository.GetReservedRooms());
    }

    /// <summary>
    /// Запрос информации о обронировании по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Полную информацию о бронировании</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReservedRooms), 200)]
    public ActionResult<ReservedRooms> Get(int id)
    {
        var reservedRoom = repository.GetReservedRoomById(id);
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
        value.Client = repositoryClient.GetClientById(reservedRoom.ClientId);
        value.Room = repositoryRoom.GetRoomById(reservedRoom.RoomId);
        value.DateDeparture = DateOnly.ParseExact(reservedRoom.DateDepart, "yyyy-mm-dd");
        value.DateArrival = DateOnly.ParseExact(reservedRoom.DateArriv, "yyyy-mm-dd");
        if (value.Client == null) { return NotFound("Клиент не найден по заданому id"); }
        if (value.Room == null) { return NotFound("Комната по заданому id не найдена"); }
        repository.PostReservedRoom(value);
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
        if(repository.GetReservedRoomById(id) == null) { return NotFound("Номер бронирования по данному id не найден"); }
        var value = mapper.Map<ReservedRooms>(reservedRoom);
        value.Client = repositoryClient.GetClientById(reservedRoom.ClientId);
        value.Room = repositoryRoom.GetRoomById(reservedRoom.RoomId);
        if (value.Client == null) { return NotFound("Клиент не найден по заданому id"); }
        if (value.Room == null) { return NotFound("Комната по заданому id не найдена"); }
        repository.PutReservedRoom(id, value);
        return Ok();
    }

    /// <summary>
    /// Удаление бронирования по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Код выполнения</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (repository.GetReservedRoomById(id) == null) { return NotFound("Номер бронирования по данному id не найден"); }
        repository.DeleteReservedRoom(id);
        return Ok();
    }


    /// <summary>
    /// Возвращает всех клиентов отеля
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("get_all_client_in_hotel/{name}")]
    public ActionResult<IEnumerable<Client>> GetAllClientInHotel(string name)
    {
        var hotelId = repositoryHotel.GetHotelIdByName(name);
        if (hotelId == null) { return NotFound("Отель с таким имененм не найден"); }
        var roomsInHotel = repositoryRoom.GetRoomsInHotel(hotelId);
        if (roomsInHotel == null) { return NotFound("Комнаты для данного отеля не найдены"); }

        return Ok(repository.ReturnAllClientInHotel(hotelId, roomsInHotel));

    }

}
