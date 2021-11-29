using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<Project> Projects { get; set; }

        public static ClientDTO ConvertToDTO(Client client)
        {
            return new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                City = client.City,
                ZipCode = client.ZipCode,
                CountryId = client.CountryId
            };
        }

        public static Client ConvertFromDTO(ClientDTO clientDTO)
        {
            return new Client
            {
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                Address = clientDTO.Address,
                City = clientDTO.City,
                ZipCode = clientDTO.ZipCode,
                CountryId = clientDTO.CountryId
            };
        }
    }
}
