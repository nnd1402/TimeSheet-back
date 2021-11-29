using System;

namespace TimeSheet.DTO
{
    public class TimeSheetEntryDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public double OverTime { get; set; }
    }
}
