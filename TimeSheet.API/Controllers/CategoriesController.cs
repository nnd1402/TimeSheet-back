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
    }
}
