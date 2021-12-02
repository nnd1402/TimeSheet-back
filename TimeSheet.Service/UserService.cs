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
    public class UserService : IUserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = userRepository.GetAll();
            var insertedEntities = userRepository.AddRange(users);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            userRepository.Save();
            return result;
        }
        public UserDTO GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            var userDTO = user.ConvertToDTO();

            return userDTO;
        }
        public UserDTO Insert(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            if (string.IsNullOrEmpty(userDTO.Username))
            {
                throw new ValidationException("Username cannot be empty");
            }
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                throw new ValidationException("Email cannot be empty");
            }
            var dtoToEntity = new User(userDTO);
            var dbUser = userRepository.Insert(dtoToEntity);
            userRepository.Save();
            var result = dbUser.ConvertToDTO();
            return result;
        }
        public void Update(int id, UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            if (string.IsNullOrEmpty(userDTO.Username))
            {
                throw new ValidationException("Username cannot be empty");
            }
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                throw new ValidationException("Email cannot be empty");
            }
            var dtoToEntity = new User(userDTO);
            userRepository.Update(id, dtoToEntity);
            userRepository.Save();
        }
        public void Delete(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException();
            }
            userRepository.Delete(id);
            userRepository.Save();
        }
    }
}
