using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class ClientService
    {
        private readonly ClientRepository clientRepository;

        public ClientService(ClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public void Delete(int id)
        {
            var client = clientRepository.GetById(id);
            if (client == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                clientRepository.Delete(id);
                clientRepository.Save();
            }
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            ICollection<ClientDTO> result = new List<ClientDTO>();
            var clients = clientRepository.GetAll();

            foreach (Client client in clients)
            {
                var clientDTO = Client.ConvertToDTO(client);
                result.Add(clientDTO);
            }
            return result;
        }

        public ClientDTO GetById(int id)
        {
            var client = clientRepository.GetById(id);
            if (client == null)
            {
                throw new NullReferenceException();
            }
            var clientDTO = Client.ConvertToDTO(client);

            return clientDTO;
        }

        public bool Insert(ClientDTO clientDTO)
        {
            bool status;
            try
            {
                var client = Client.ConvertFromDTO(clientDTO);
                if (client == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    clientRepository.Insert(client);
                    clientRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, ClientDTO clientDTO)
        {
            bool status;
            try
            {
                var client = Client.ConvertFromDTO(clientDTO);
                client = clientRepository.GetById(id);
                if (id == 0 || client == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    clientRepository.Update(id, client);
                    clientRepository.Save();
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
