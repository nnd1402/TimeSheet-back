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
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            var countries = _countryRepository.GetAll();
            var result = countries.ToList().ConvertAll(e => e.ConvertToDTO());
            _countryRepository.Save();
            return result;
        }
        public CountryDTO GetById(int id)
        {
            var country = _countryRepository.GetById(id);
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
            var dbCountry = _countryRepository.Insert(dtoToEntity);
            _countryRepository.Save();
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
            _countryRepository.Update(id, dtoToEntity);
            _countryRepository.Save();
        }
        public void Delete(int id)
        {
            var country = _countryRepository.GetById(id);
            if (country == null)
            {
                throw new NotFoundException();
            }
            _countryRepository.Delete(id);
            _countryRepository.Save();
        }
    }
}
