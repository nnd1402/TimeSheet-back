using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            var roles = _roleRepository.GetAll();
            var result = roles.ToList().ConvertAll(e => e.ConvertToDTO());
            _roleRepository.Save();
            return result;
        }
      
    }
}
