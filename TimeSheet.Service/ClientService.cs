using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class ClientService : IClientService
    {
        private readonly ClientRepository clientRepository;

        public ClientService(ClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            var clients = clientRepository.GetAll();
            var insertedEntities = clientRepository.AddRange(clients);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            clientRepository.Save();
            return result;
        }
        public ClientDTO GetById(int id)
        {
            var client = clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException();
            }
            var clientDTO = client.ConvertToDTO();

            return clientDTO;
        }
        public ClientDTO Insert(ClientDTO clientDTO)
        {
            if (string.IsNullOrEmpty(clientDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            if (string.IsNullOrEmpty(clientDTO.Address))
            {
                throw new ValidationException("Address cannot be empty");
            }
            if (string.IsNullOrEmpty(clientDTO.City))
            {
                throw new ValidationException("City cannot be empty");
            }
            var dtoToEntity = new Client(clientDTO);
            var dbClient = clientRepository.Insert(dtoToEntity);
            clientRepository.Save();
            var result = dbClient.ConvertToDTO();
            return result;
        }
        public void Update(int id, ClientDTO clientDTO)
        {
            if (string.IsNullOrEmpty(clientDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            if (string.IsNullOrEmpty(clientDTO.Address))
            {
                throw new ValidationException("Address cannot be empty");
            }
            if (string.IsNullOrEmpty(clientDTO.City))
            {
                throw new ValidationException("City cannot be empty");
            }
            var dtoToEntity = new Client(clientDTO);
            clientRepository.Update(id, dtoToEntity);
            clientRepository.Save();
        }
        public void Delete(int id)
        {
            var client = clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException();
            }
            clientRepository.Delete(id);
            clientRepository.Save();
        }
    }
}
