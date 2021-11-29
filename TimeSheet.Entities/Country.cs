using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

        public static CountryDTO ConvertToDTO(Country country)
        {
            return new CountryDTO
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public static Country ConvertFromDTO(CountryDTO countryDTO)
        {
            return new Country
            {
                Id = countryDTO.Id,
                Name = countryDTO.Name,
            };
        }
    }
}
