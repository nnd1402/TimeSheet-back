using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class TimeSheetEntryService
    {
        private readonly TimeSheetEntryRepository timeSheetEntryRepository;

        public TimeSheetEntryService(TimeSheetEntryRepository timeSheetEntryRepository)
        {
            this.timeSheetEntryRepository = timeSheetEntryRepository;
        }

        public void Delete(int id)
        {
            var entry = timeSheetEntryRepository.GetById(id);
            if (entry == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                timeSheetEntryRepository.Delete(id);
                timeSheetEntryRepository.Save();
            }
        }

        public IEnumerable<TimeSheetEntryDTO> GetAll()
        {
            ICollection<TimeSheetEntryDTO> result = new List<TimeSheetEntryDTO>();
            var entries = timeSheetEntryRepository.GetAll();

            foreach (TimeSheetEntry entry in entries)
            {
                var entryDTO = TimeSheetEntry.ConvertToDTO(entry);
                result.Add(entryDTO);
            }
            return result;


        }

        public TimeSheetEntryDTO GetById(int id)
        {
            var entry = timeSheetEntryRepository.GetById(id);
            if (entry == null)
            {
                throw new NullReferenceException();
            }
            var entryDTO = TimeSheetEntry.ConvertToDTO(entry);

            return entryDTO;
        }

        public bool Insert(TimeSheetEntryDTO entryDTO)
        {
            bool status;
            try
            {
                var entry = TimeSheetEntry.ConvertFromDTO(entryDTO);
                if (entry == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    timeSheetEntryRepository.Insert(entry);
                    timeSheetEntryRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, TimeSheetEntryDTO entryDTO)
        {
            bool status;
            try
            {
                var entry = TimeSheetEntry.ConvertFromDTO(entryDTO);
                entry = timeSheetEntryRepository.GetById(id);
                if (id == 0 || entry == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    timeSheetEntryRepository.Update(id, entry);
                    timeSheetEntryRepository.Save();
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
