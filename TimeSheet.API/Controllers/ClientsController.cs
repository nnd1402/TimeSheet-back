using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(_clientService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetClient(int id)
        {
            try
            {
                return Ok(_clientService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Client with the Id: {id} was not found");
            }
        }

        [HttpPost]
        public ActionResult AddClient(ClientDTO clientDTO)
        {
            try
            {
                var insertedClient = _clientService.Insert(clientDTO);
                return Ok(insertedClient);
            }
            catch(ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                _clientService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Client with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditClient(int id, ClientDTO clientDTO)
        {
            try
            {
                _clientService.Update(id, clientDTO);
                return Ok(clientDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}
