using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public void Delete(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                userRepository.Delete(id);
                userRepository.Save();
            }
        }
        public IEnumerable<UserDTO> GetAll()
        {
            ICollection<UserDTO> result = new List<UserDTO>();
            var users = userRepository.GetAll();

            foreach (User user in users)
            {
                var userDTO = User.ConvertToDTO(user);
                result.Add(userDTO);
            }
            return result;
        }

        public UserDTO GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            var userDTO = User.ConvertToDTO(user);

            return userDTO;
        }

        public bool Insert(UserDTO userDTO)
        {
            bool status;
            try
            {
                var user = User.ConvertFromDTO(userDTO);
                if (user == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    userRepository.Insert(user);
                    userRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool Update(int id, UserDTO userDTO)
        {
            bool status;
            try
            {
                var user = User.ConvertFromDTO(userDTO);
                user = userRepository.GetById(id);
                if (id == 0 || user == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    userRepository.Update(id, user);
                    userRepository.Save();
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
