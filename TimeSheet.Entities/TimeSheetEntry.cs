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

        public TimeSheetEntryDTO ConvertToDTO()
        {
            return new TimeSheetEntryDTO
            {
                Id = this.Id,
                UserId = this.UserId,
                ProjectId = this.ProjectId,
                CategoryId = this.CategoryId,
                Description = this.Description,
                Date = this.Date,
                Time = this.Time,
                OverTime = this.OverTime
            };
        }

        public TimeSheetEntry (TimeSheetEntryDTO timeSheetEntryDTO)
        {
            Id = timeSheetEntryDTO.Id;
            UserId = timeSheetEntryDTO.UserId;
            ProjectId = timeSheetEntryDTO.ProjectId;
            CategoryId = timeSheetEntryDTO.CategoryId;
            Description = timeSheetEntryDTO.Description;
            Date = timeSheetEntryDTO.Date;
            Time = timeSheetEntryDTO.Time;
            OverTime = timeSheetEntryDTO.OverTime;
        }
    }
}
