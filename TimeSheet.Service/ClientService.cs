using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICountryRepository _countryRepository;

        public ClientService(IClientRepository clientRepository, ICountryRepository countryRepository)
        {
            this._clientRepository = clientRepository;
            this._countryRepository = countryRepository;
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            var clients = _clientRepository.GetAll();
            var result = clients.ToList().ConvertAll(e => e.ConvertToDTO());
            _clientRepository.Save();
            return result;
        }
        public ClientDTO GetById(int id)
        {
            var client = _clientRepository.GetById(id);
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
            var country = _countryRepository.GetById(clientDTO.CountryId);
            if(country == null)
            {
                throw new ValidationException("Country does not exist");
            }
            var clientByName = _clientRepository.Search(c => c.Name == clientDTO.Name);
            if (clientByName.Count() > 0)
            {
                throw new ValidationException("Client with that name already exists");
            }
            var dtoToEntity = new Client(clientDTO);
            var dbClient = _clientRepository.Insert(dtoToEntity);
            _clientRepository.Save();
            var result = dbClient.ConvertToDTO();
            return result;
        }
        public void Update(int id, ClientDTO clientDTO)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException();
            }
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
            var country = _countryRepository.GetById(clientDTO.CountryId);
            if (country == null)
            {
                throw new ValidationException("Country does not exist");
            }
            
            var dtoToEntity = new Client(clientDTO);
            _clientRepository.Update(id, dtoToEntity);
            _clientRepository.Save();
        }
        public void Delete(int id)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                throw new NotFoundException();
            }
            _clientRepository.Delete(id);
            _clientRepository.Save();
        }
    }
}
