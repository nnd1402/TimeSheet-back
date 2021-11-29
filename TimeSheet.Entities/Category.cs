using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }

        public static CategoryDTO ConvertToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static Category ConvertFromDTO(CategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
            };
        }
    }

}
