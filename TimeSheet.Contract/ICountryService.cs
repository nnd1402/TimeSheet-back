﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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