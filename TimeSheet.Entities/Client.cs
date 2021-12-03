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

        public Client()
        {

        }

        public ClientDTO ConvertToDTO()
        {
            return new ClientDTO
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                City = this.City,
                ZipCode = this.ZipCode,
                CountryId = this.CountryId
            };
        }

        public Client (ClientDTO clientDTO)
        {
            Id = clientDTO.Id;
            Name = clientDTO.Name;
            Address = clientDTO.Address;
            City = clientDTO.City;
            ZipCode = clientDTO.ZipCode;
            CountryId = clientDTO.CountryId;
        }
    }
}
