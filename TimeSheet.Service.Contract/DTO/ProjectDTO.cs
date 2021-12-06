using System.Collections.Generic;

namespace TimeSheet.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int LeadUserId { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }
}