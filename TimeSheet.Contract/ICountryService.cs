using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetAll();
        CountryDTO GetById(int id);
        CountryDTO Insert(CountryDTO countryDTO);
        void Update(int Id, CountryDTO countryDTO);
        void Delete(int id);
    }
}
