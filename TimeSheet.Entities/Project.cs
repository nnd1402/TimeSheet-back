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

        public ProjectDTO ConvertToDTO()
        {
            return new ProjectDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                ClientId = this.ClientId
            };
        }

        public Project (ProjectDTO projectDTO)
        {
            Id = projectDTO.Id;
            Name = projectDTO.Name;
            Description = projectDTO.Description;
            ClientId = projectDTO.ClientId;
        }

    }
}
