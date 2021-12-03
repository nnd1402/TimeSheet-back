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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _userRepository.GetAll();
            var result = users.ToList().ConvertAll(e => e.ConvertToDTO());
            _userRepository.Save();
            return result;
        }
        public UserDTO GetById(int id)
        {
            var user = _userRepository.GetById(id);
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
            var dbUser = _userRepository.Insert(dtoToEntity);
            _userRepository.Save();
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
            _userRepository.Update(id, dtoToEntity);
            _userRepository.Save();
        }
        public void Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException();
            }
            _userRepository.Delete(id);
            _userRepository.Save();
        }
    }
}
