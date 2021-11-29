using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class CountryService
    {
        private readonly CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public void Delete(int id)
        {
            var country = countryRepository.GetById(id);
            if (country == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                countryRepository.Delete(id);
                countryRepository.Save();
            }
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            ICollection<CountryDTO> result = new List<CountryDTO>();
            var countries = countryRepository.GetAll();

            foreach (Country country in countries)
            {
                var countryDTO = Country.ConvertToDTO(country);
                result.Add(countryDTO);
            }
            return result;
        }

        public CountryDTO GetById(int id)
        {
            var country = countryRepository.GetById(id);
            if (country == null)
            {
                throw new NullReferenceException();
            }
            var countryDTO = Country.ConvertToDTO(country);

            return countryDTO;
        }

        public bool Insert(CountryDTO countryDTO)
        {
            bool status;
            try
            {
                var country = Country.ConvertFromDTO(countryDTO);
                if (country == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    countryRepository.Insert(country);
                    countryRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, CountryDTO countryDTO)
        {
            bool status;
            try
            {
                var country = Country.ConvertFromDTO(countryDTO);
                country = countryRepository.GetById(id);
                if (id == 0 || country == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    countryRepository.Update(id, country);
                    countryRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
