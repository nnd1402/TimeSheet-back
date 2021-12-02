using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using TimeSheet.Contract;
using TimeSheet.DTO;

namespace TimeSheet.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService IclientService)
        {
            clientService = IclientService;
        }

        [HttpGet]
        [Route("api/clients")]
        public IActionResult GetClients()
        {
            return Ok(clientService.GetAll());
        }

        [HttpGet]
        [Route("api/clients/{id}")]
        public IActionResult GetClient(int id)
        {
            var existingClient = clientService.GetById(id);
            if(existingClient != null)
            {
                return Ok(clientService.GetById(id));
            }
            return NotFound($"Client with the Id: {id} was not found");
        }

        [HttpPost]
        [Route("api/clients")]
        public ActionResult AddClient(ClientDTO clientDTO)
        {
            clientService.Insert(clientDTO);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + clientDTO.Id), clientDTO);
        }

        [HttpDelete]
        [Route("api/clients/{id}")]
        public ActionResult DeleteClient(int id)
        {
            var existingClient = clientService.GetById(id);
            if(existingClient != null)
            {
                clientService.Delete(id);
                return Ok();
            }
            return NotFound($"Client with the Id: {id} was not found");
        }

        [HttpPut]
        [Route("api/clients/{id}")]
        public ActionResult EditClient(int id, ClientDTO clientDTO)
        {
            var existingClient = clientService.GetById(id);
            if (existingClient != null)
            {
                clientService.Update(id, clientDTO);
            }
            return Ok(clientDTO);
        }
    }
}
