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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoriesService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoriesService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categoriesService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                return Ok(_categoriesService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Category with the Id: {id} was not found");
            }
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var insertedCategory = _categoriesService.Insert(categoryDTO);
                return Created(new Uri(Request.GetEncodedUrl() + "/" + insertedCategory.Id), insertedCategory);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                _categoriesService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Category with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditCategory(int id, CategoryDTO categoryDTO)
        {
            try
            {
                _categoriesService.Update(id, categoryDTO);
                return Ok(categoryDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}
