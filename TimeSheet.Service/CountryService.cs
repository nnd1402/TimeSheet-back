using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class CountryService : ICountryService
    {
        private readonly CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            var countries = countryRepository.GetAll();
            var insertedEntities = countryRepository.AddRange(countries);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            countryRepository.Save();
            return result;
        }
        public CountryDTO GetById(int id)
        {
            var country = countryRepository.GetById(id);
            if (country == null)
            {
                throw new NotFoundException();
            }
            var countryDTO = country.ConvertToDTO();

            return countryDTO;
        }
        public CountryDTO Insert(CountryDTO countryDTO)
        {
            if (countryDTO == null)
            {
                throw new ValidationException("Country must be provided");
            }
            if (string.IsNullOrEmpty(countryDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Country(countryDTO);
            var dbCountry = countryRepository.Insert(dtoToEntity);
            countryRepository.Save();
            var result = dbCountry.ConvertToDTO();
            return result;
        }
        public void Update(int id, CountryDTO countryDTO)
        {
            if (string.IsNullOrEmpty(countryDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Country(countryDTO);
            countryRepository.Update(id, dtoToEntity);
            countryRepository.Save();
        }
        public void Delete(int id)
        {
            var country = countryRepository.GetById(id);
            if (country == null)
            {
                throw new NotFoundException();
            }
            countryRepository.Delete(id);
            countryRepository.Save();
        }
    }
}
