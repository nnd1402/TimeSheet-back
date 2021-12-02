using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface IClientService
    {
        IEnumerable<ClientDTO> GetAll();
        ClientDTO GetById(int id);
        ClientDTO Insert(ClientDTO clientDTO);
        void Update(int Id, ClientDTO clientDTO);
        void Delete(int id);
    }
}
