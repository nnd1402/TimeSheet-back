using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetEntriesController : ControllerBase
    {
        private readonly ITimeSheetEntryService _timeSheetEntryService;

        public TimeSheetEntriesController(ITimeSheetEntryService timeSheetEntryService)
        {
            this._timeSheetEntryService = timeSheetEntryService;
        }

        [HttpGet]
        public ActionResult GetEntries([FromQuery(Name = "date")] DateTime? date)
        {
            if (date.HasValue)
            {
                return Ok(_timeSheetEntryService.GetByDate(date.Value));
            }
            return Ok(_timeSheetEntryService.GetAll());
        }

        [HttpPost]
        public IActionResult InsertMany(IEnumerable<TimeSheetEntryDTO> entries)
        {
            try
            {
                var insertedEntries = _timeSheetEntryService.InsertMany(entries);
                return Ok(insertedEntries);
            }
            catch(ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteEntry(int id)
        {
            try
            {
                _timeSheetEntryService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Timesheet entry with the Id: {id} was not found");
            }
        }
        [HttpPut]
        [Route("{date}")]
        public IActionResult Update(DateTime date, IEnumerable<TimeSheetEntryDTO> entries)
        {
            try
            {
                _timeSheetEntryService.Update(date, entries);
                return Ok(entries);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }
    }
}

