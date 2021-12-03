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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            this._countryService = countryService;
        }

        [HttpGet]
        public ActionResult GetCountries()
        {
            return Ok(_countryService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCountry(int id)
        {
            try
            {
                return Ok(_countryService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Country with the Id: {id} was not found");
            }
        }

        [HttpPost]
        public ActionResult AddCountry(CountryDTO countryDTO)
        {
            try
            {   
                var insertedCountry = _countryService.Insert(countryDTO);
                
                return Created(new Uri(Request.GetEncodedUrl() + "/" + insertedCountry.Id), insertedCountry);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCountry(int id)
        {
            try
            {
                _countryService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Country with the Id: {id} was not found");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult EditCountry(int id, CountryDTO countryDTO)
        {
            try
            {
                _countryService.Update(id, countryDTO);
                return Ok(countryDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

    }
}
