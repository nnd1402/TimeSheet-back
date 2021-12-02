using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            var roles = roleRepository.GetAll();
            var insertedEntities = roleRepository.AddRange(roles);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            roleRepository.Save();
            return result;
        }
        public RoleDTO GetById(int id)
        {
            var role = roleRepository.GetById(id);
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
            var dbRole = roleRepository.Insert(dtoToEntity);
            roleRepository.Save();
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
            roleRepository.Update(id, dtoToEntity);
            roleRepository.Save();
        }
        public void Delete(int id)
        {
            var role = roleRepository.GetById(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            roleRepository.Delete(id);
            roleRepository.Save();
        }
    }
}
