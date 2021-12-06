using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Repository.Contract;

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
      
    }
}
