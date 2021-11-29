using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public ICollection<UserOnProject> UsersOnProjects { get; set; }

        public static ProjectDTO ConvertToDTO(Project project)
        {
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ClientId = project.ClientId
            };
        }

        public static Project ConvertFromDTO(ProjectDTO projectDTO)
        {
            return new Project
            {
                Id = projectDTO.Id,
                Name = projectDTO.Name,
                Description = projectDTO.Description,
                ClientId = projectDTO.ClientId
            };
        }

    }
}
