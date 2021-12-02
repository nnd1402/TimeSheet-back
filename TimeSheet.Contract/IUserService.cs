using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(int id);
        UserDTO Insert(UserDTO userDTO);
        void Update(int Id, UserDTO userDTO);
        void Delete(int id);
    }
}
