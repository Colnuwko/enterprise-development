using Microsoft.AspNetCore.Mvc;
using HotelBookingDetails.Domain;
using HotelBookingDetails.Domain.Repositories;
using WebApi.Dto;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepositoryClient repository, IMapper mapper) : ControllerBase
{
    
    
    /// <summary>
    /// fsdfsdfdsf
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repository.GetClients());
    }

    // GET api/<ClientController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Client), 200)]
    public ActionResult<Client> Get(int id)
    {
        var client = repository.GetClientById(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    // POST api/<ClientController>
    [HttpPost]
    public IActionResult Post([FromBody] ClientDto client)
    {
        var value = mapper.Map<Client>(client);
        repository.PostClient(value);
        return Ok();
    }

    // PUT api/<ClientController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Client client)
    {
        repository.PutClient(id, client);
        return Ok();
    }

    // DELETE api/<ClientController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        repository.DeleteClient(id);
        return Ok();
    }
}
