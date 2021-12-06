using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface ICountryService
    {
        IEnumerable<CountryDTO> GetAll();
    }
}
