using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
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
        if (value.DateDeparture != null) { DateOnly.ParseExact(reservedRoom.DateDeparture, "yyyy-mm-dd"); }
        value.DateArrival = DateOnly.ParseExact(reservedRoom.DateArrival, "yyyy-mm-dd");
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
            DateOnly.ParseExact(reservedRoom.DateDeparture, "yyyy-mm-dd"); 
        value.DateArrival = DateOnly.ParseExact(reservedRoom.DateArrival, "yyyy-mm-dd");
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
    /// <returns>Список клиентов</returns>
    [HttpGet("get_all_client_in_hotel/{name}")]
    public ActionResult<IEnumerable<Client>> GetAllClientInHotel(string name)
    {
        var hotelId = GetHotelIdByName(name);
        if (hotelId == null) 
            return NotFound("Отель с таким имененм не найден"); 
        var roomsInHotel = GetRoomsInHotel([hotelId]);
        if (roomsInHotel == null) 
            return NotFound("Комнаты для данного отеля не найдены"); 

        return Ok(ReturnAllClientInHotel(hotelId, roomsInHotel));
    }

    /// <summary>
    /// Возвращает все свободные комнаты в выбранном городе
    /// </summary>
    /// <param name="city"></param>
    /// <returns>Список свободных комнат</returns>
    [HttpGet("get_all_free_rooms/{city}")]
    public ActionResult<IEnumerable<Room>> GetFreeRoomInCity(string city)
    {
        var hotelsInCity = GetHotelsByCity(city); 
        if (hotelsInCity.Count() == 0) 
            return NotFound("Отели в выбранном городе не найдены");  
        var roomsInCity = GetRoomsInHotel(hotelsInCity);
        return Ok(GetFreeRoomInCity(roomsInCity));
    }

    /// <summary>
    /// Возвращает клиентов с наибольшим временем проживания в отелях
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet("get_clients_with_the_longest_hotel_stays")]
    public ActionResult<IEnumerable<M>> GetLongLiversClient()
    {
        return Ok(GetLongLiversHotel());
    }

    [NonAction]
    public IEnumerable<Client> ReturnAllClientInHotel(int hotelId, IEnumerable<Room> rooms)
    {
        var clientInHotel = repository.GetAll()
            .OrderBy(r => r.Client.FullName)
            .Where(r => rooms.ToList().Contains(r.Room))
            .Select(r => r.Client)
            .ToList();
        return clientInHotel;
    }

    [NonAction]
    public IEnumerable<Room> GetFreeRoomInCity(IEnumerable<Room> rooms)
    {
        var reservRooms = repository.GetAll().Where(r => rooms.Contains(r.Room) && r.DateDeparture == null).Select(r => r.Room);
        var freeRooms = rooms.Where(r => !reservRooms.Contains(r)).Select(r => r);
        return freeRooms;
    }

    [NonAction]
    public IEnumerable<M> GetLongLiversHotel()
    {
        var longerPeriods = repository.GetAll()
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Max(c => c.Total);

        var clientWithLongerPer = repository.GetAll()
            .GroupBy(c => c.Client)
            .Select(c => new
            {
                Client = c.Key,
                Total = c.Sum(r => r.Period)
            }).Where(c => c.Total == longerPeriods).Select(c => new M (c.Client, c.Total)).ToList();
        return clientWithLongerPer;
    }

    [NonAction]
    public int GetHotelIdByName(string name)
    {
        return repositoryHotel.GetAll().Where(h => h.Name == name).Select(h => h.Id).First();
    }

    [NonAction]
    public IEnumerable<int> GetHotelsByCity(string city) => repositoryHotel.GetAll().Where(h => h.City == city).Select(h => h.Id);

    [NonAction]
    public IEnumerable<Room> GetRoomsInHotel(IEnumerable<int> id) => repositoryRoom.GetAll().Where(r => id.Contains(r.HotelId)).Select(r => r);
}