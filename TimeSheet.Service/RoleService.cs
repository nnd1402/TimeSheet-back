using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class RoleService
    {
        private readonly RoleRepository roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void Delete(int id)
        {
            var role = roleRepository.GetById(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                roleRepository.Delete(id);
                roleRepository.Save();
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            ICollection<RoleDTO> result = new List<RoleDTO>();
            var roles = roleRepository.GetAll();

            foreach (Role role in roles)
            {
                var roleDTO = Role.ConvertToDTO(role);
                result.Add(roleDTO);
            }
            return result;
        }

        public RoleDTO GetById(int id)
        {
            var role = roleRepository.GetById(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            var roleDTO = Role.ConvertToDTO(role);

            return roleDTO;
        }

        public bool Insert(RoleDTO roleDTO)
        {
            bool status;
            try
            {
                var role = Role.ConvertFromDTO(roleDTO);
                if (role == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    roleRepository.Insert(role);
                    roleRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, RoleDTO roleDTO)
        {
            bool status;
            try
            {
                var role = Role.ConvertFromDTO(roleDTO);
                role = roleRepository.GetById(id);
                if (id == 0 || role == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    roleRepository.Update(id, role);
                    roleRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
