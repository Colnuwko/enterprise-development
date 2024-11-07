using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservedRoomController(IRepository<ReservedRooms> repository, IRepository<Room> repositoryRoom, IRepository<Client> repositoryClient,
    IRepository<Hotel> repositoryHotel, IMapper mapper) : ControllerBase
{


    /// <summary>
    /// Запрос возвращающий полную информацию обо всех бронированиях
    /// </summary>
    /// <returns>список бронирований</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ReservedRooms>> Get()
    {
        return Ok(repository.GetAll());
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
        var reservedRoom = repository.GetById(id);
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
        value.Client = repositoryClient.GetById(reservedRoom.ClientId);
        value.Room = repositoryRoom.GetById(reservedRoom.RoomId);
        if (value.DateDeparture != null) { DateOnly.ParseExact(reservedRoom.DateDepart, "yyyy-mm-dd"); }
        value.DateArrival = DateOnly.ParseExact(reservedRoom.DateArriv, "yyyy-mm-dd");
        if (value.Client == null) 
            return NotFound("Клиент не найден по заданному id"); 
        if (value.Room == null) 
            return NotFound("Комната по заданному id не найдена"); 
        repository.Post(value);
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
        if(repository.GetById(id) == null) 
            return NotFound("Номер бронирования по данному id не найден"); 
        var value = mapper.Map<ReservedRooms>(reservedRoom);
        value.Client = repositoryClient.GetById(reservedRoom.ClientId);
        value.Room = repositoryRoom.GetById(reservedRoom.RoomId);
        if (value.DateDeparture != null) 
            DateOnly.ParseExact(reservedRoom.DateDepart, "yyyy-mm-dd"); 
        value.DateArrival = DateOnly.ParseExact(reservedRoom.DateArriv, "yyyy-mm-dd");
        if (value.Client == null) 
            return NotFound("Клиент не найден по заданному id"); 
        if (value.Room == null) 
            return NotFound("Комната по заданному id не найдена"); 
        repository.Put(id, value);
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
        if (repository.GetById(id) == null) 
            return NotFound("Номер бронирования по данному id не найден"); 
        repository.Delete(id);
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
        if (hotelId == null) 
            return NotFound("Отель с таким имененм не найден"); 
        var roomsInHotel = repositoryRoom.GetRoomsInHotel([hotelId]);
        if (roomsInHotel == null) 
            return NotFound("Комнаты для данного отеля не найдены"); 

        return Ok(repository.ReturnAllClientInHotel(hotelId, roomsInHotel));

    }

    /// <summary>
    /// Возвращает все свободные комнаты в выбранном городе
    /// </summary>
    /// <param name="city"></param>
    /// <returns></returns>
    [HttpGet("get_all_free_rooms/{city}")]
    public ActionResult<IEnumerable<Room>> GetFreeRoomInCity(string city)
    {

        var hotelsInCity = repositoryHotel.GetHotelsByCity(city); 
        if (hotelsInCity.Count() == 0) 
            return NotFound("Отели в выбранном городе не найдены");  
        var roomsInCity = repositoryRoom.GetRoomsInHotel(hotelsInCity);

        return Ok(repository.GetFreeRoomInCity(roomsInCity));
    }

    /// <summary>
    /// Возвращает клиентов с наибольшим временем проживания
    /// </summary>
    /// <returns></returns>
    [HttpGet("get_all_free_rooms")]
    public ActionResult<IEnumerable<Client>> GetLongLiversClient()
    {
        return Ok(repository.GetLongLiversHotel());
    }

}
