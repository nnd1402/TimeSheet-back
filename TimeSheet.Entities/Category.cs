using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }

        public CategoryDTO ConvertToDTO()
        {
            return new CategoryDTO
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public Category(CategoryDTO categoryDTO)
        {
            Id = categoryDTO.Id;
            Name = categoryDTO.Name;
        }
    }

}
