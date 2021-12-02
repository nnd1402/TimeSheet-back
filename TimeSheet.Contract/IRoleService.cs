using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetAll();
        RoleDTO GetById(int id);
        RoleDTO Insert(RoleDTO roleDTO);
        void Update(int Id, RoleDTO roleDTO);
        void Delete(int id);
    }
}
