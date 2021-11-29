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

        public static UserDTO ConvertToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                HoursPerWeek = user.HoursPerWeek,
                Status = user.Status,
                RoleId = user.RoleId
            };
        }

        public static User ConvertFromDTO(UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = userDTO.Password,
                HoursPerWeek = userDTO.HoursPerWeek,
                Status = userDTO.Status,
                RoleId = userDTO.RoleId
            };
        }

        public static UserLogin ConvertoToDTO (User user)
        {
            return new UserLogin
            {
                Username = user.Username,
                Password = user.Password
            };
        }

        public static User ConvertoFromDTO(UserLogin userLogin)
        {
            return new User
            {
                Username = userLogin.Username,
                Password = userLogin.Password
            };
        }
    }
}
