using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetAll();
    }
}
