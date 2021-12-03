using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

        public Country()
        {

        }

        public CountryDTO ConvertToDTO()
        {
            return new CountryDTO
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public Country (CountryDTO countryDTO)
        {
            Id = countryDTO.Id;
            Name = countryDTO.Name;
        }
    }
}
