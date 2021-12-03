using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

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
        public RoleDTO GetById(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                throw new NotFoundException();
            }
            var roleDTO = role.ConvertToDTO();

            return roleDTO;
        }
        public RoleDTO Insert(RoleDTO roleDTO)
        {
            if (string.IsNullOrEmpty(roleDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Role(roleDTO);
            var dbRole = _roleRepository.Insert(dtoToEntity);
            _roleRepository.Save();
            var result = dbRole.ConvertToDTO();
            return result;
        }
        public void Update(int id, RoleDTO roleDTO)
        {
            if (string.IsNullOrEmpty(roleDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Role(roleDTO);
            _roleRepository.Update(id, dtoToEntity);
            _roleRepository.Save();
        }
        public void Delete(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            _roleRepository.Delete(id);
            _roleRepository.Save();
        }
    }
}
