using Microsoft.AspNetCore.Http;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"User with the Id: {id} was not found");
            }

        }

        [HttpPost]
        public ActionResult AddUser(UserDTO userDTO)
        {
            try
            {
                var insertedUser = _userService.Insert(userDTO);
                return Ok(insertedUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"User with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditUser(int id, UserDTO userDTO)
        {
            try
            {
                _userService.Update(id, userDTO);
                return Ok(userDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}
