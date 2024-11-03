//using Microsoft.AspNetCore.Mvc;
//using HotelBookingDetails.Domain;
//using HotelBookingDetails.Domain.Repositories;
//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApi.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class ClientController(IRepository repository) : ControllerBase
//{
    
//    // GET: api/<ClientController>
//    [HttpGet]
//    public ActionResult<IEnumerable<Client>> Get()
//    {
//        return Ok(repository.GetClients());
//    }

//    // GET api/<ClientController>/5
//    [HttpGet("{id}")]
//    [ProducesResponseType(typeof(Client), 200)]
//    public ActionResult<Client> Get(int id)
//    {
//        var client = repository.GetClientById(id);
//        if (client == null)
//            return NotFound();

//        return Ok(client);
//    }

//    // POST api/<ClientController>
//    [HttpPost]
//    public IActionResult Post([FromBody] Client client)
//    {
//        repository.PostClient(client);
//        return Ok();
//    }

//    // PUT api/<ClientController>/5
//    [HttpPut("{id}")]
//    public IActionResult Put(int id, [FromBody] Client client)
//    {
//        repository.PutClient(id, client);
//        return Ok();
//    }

//    // DELETE api/<ClientController>/5
//    [HttpDelete("{id}")]
//    public IActionResult Delete(int id)
//    {
//        repository.DeleteClient(id);
//        return Ok();
//    }
//}
