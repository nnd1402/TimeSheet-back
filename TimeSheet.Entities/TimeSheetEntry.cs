using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class TimeSheetEntry
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("UserId, ProjectId")]
        public virtual UserOnProject UsersOnProjects { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Time { get; set; }

        public double OverTime { get; set; }

        public static TimeSheetEntryDTO ConvertToDTO(TimeSheetEntry timeSheetEntry)
        {
            return new TimeSheetEntryDTO
            {
                Id = timeSheetEntry.Id,
                UserId = timeSheetEntry.UserId,
                ProjectId = timeSheetEntry.ProjectId,
                CategoryId = timeSheetEntry.CategoryId,
                Description = timeSheetEntry.Description,
                Date = timeSheetEntry.Date,
                Time = timeSheetEntry.Time,
                OverTime = timeSheetEntry.OverTime
            };
        }

        public static TimeSheetEntry ConvertFromDTO(TimeSheetEntryDTO timeSheetEntryDTO)
        {
            return new TimeSheetEntry
            {
                Id = timeSheetEntryDTO.Id,
                UserId = timeSheetEntryDTO.UserId,
                ProjectId = timeSheetEntryDTO.ProjectId,
                CategoryId = timeSheetEntryDTO.CategoryId,
                Description = timeSheetEntryDTO.Description,
                Date = timeSheetEntryDTO.Date,
                Time = timeSheetEntryDTO.Time,
                OverTime = timeSheetEntryDTO.OverTime
            };
        }
    }
}
