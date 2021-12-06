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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(_projectService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProject(int id)
        {
            try
            {
                return Ok(_projectService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Project with the Id: {id} was not found");
            }

        }

        [HttpPost]
        public ActionResult AddProject(ProjectDTO projectDTO)
        {
            try
            {
                var insertedProject = _projectService.Insert(projectDTO);
                return Ok(insertedProject);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProject(int id)
        {
            try
            {
                _projectService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Project with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditProject(int id, ProjectDTO projectDTO)
        {
            try
            {
                _projectService.Update(id, projectDTO);
                return Ok(projectDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}
