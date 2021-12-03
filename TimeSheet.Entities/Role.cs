using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {

        }

        public RoleDTO ConvertToDTO()
        {
            return new RoleDTO
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public Role (RoleDTO roleDTO)
        {
            Id = roleDTO.Id;
            Name = roleDTO.Name;
        }
    }
}
