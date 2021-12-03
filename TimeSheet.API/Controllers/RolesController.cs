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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_roleService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRole(int id)
        {
            try
            {
                return Ok(_roleService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Role with the Id: {id} was not found");
            }

        }

        [HttpPost]
        public ActionResult AddRole(RoleDTO roleDTO)
        {
            try
            {
                var insertedRole = _roleService.Insert(roleDTO);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + insertedRole.Id), insertedRole);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteRole(int id)
        {
            try
            {
                _roleService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Role with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditRole(int id, RoleDTO roleDTO)
        {
            try
            {
                _roleService.Update(id, roleDTO);
                return Ok(roleDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}
