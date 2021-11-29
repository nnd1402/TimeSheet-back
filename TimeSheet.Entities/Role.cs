using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public static RoleDTO ConvertToDTO(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ConvertFromDTO(RoleDTO roleDTO)
        {
            return new Role
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };
        }
    }
}
