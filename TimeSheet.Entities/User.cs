using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public double HoursPerWeek { get; set; }

        public bool Status { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public ICollection<UserOnProject> UsersOnProjects { get; set; }

        public User()
        {

        }

        public UserDTO ConvertToDTO()
        {
            return new UserDTO
            {
                Id = this.Id,
                Name = this.Name,
                Username = this.Username,
                Email = this.Email,
                HoursPerWeek = this.HoursPerWeek,
                Status = this.Status,
                RoleId = this.RoleId
            };
        }
        public User(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Name = userDTO.Name;
            Username = userDTO.Username;
            Email = userDTO.Email;
            HoursPerWeek = userDTO.HoursPerWeek;
            Status = userDTO.Status;
            RoleId = userDTO.RoleId;
        }
    }
}
