using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;
using System.Linq;
using TimeSheet.Contract;

namespace TimeSheet.Service
{
    public class TimeSheetEntryService : ITimeSheetEntryService
    {
        private readonly TimeSheetEntryRepository timeSheetEntryRepository;
        private readonly UserOnProjectRepository userOnProjectRepository;

        public TimeSheetEntryService(TimeSheetEntryRepository timeSheetEntryRepository)
        {
            this.timeSheetEntryRepository = timeSheetEntryRepository;
        }

        public IEnumerable<TimeSheetEntryDTO> GetAll()
        {
            var entries = timeSheetEntryRepository.GetAll();
            var insertedEntities = timeSheetEntryRepository.AddRange(entries);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            timeSheetEntryRepository.Save();
            return result;
        }

        public TimeSheetEntryDTO GetById(int id)
        {
            var entry = timeSheetEntryRepository.GetById(id);
            if (entry == null)
            {
                throw new NotFoundException();
            }
            var entryDTO = entry.ConvertToDTO();

            return entryDTO;
        }

        public IEnumerable<TimeSheetEntryDTO> InsertMany(IEnumerable<TimeSheetEntryDTO> entries)
        {
            ICollection<TimeSheetEntryDTO> results = new List<TimeSheetEntryDTO>();
            foreach (TimeSheetEntryDTO entryDTO in entries)
            {
                var userOnProject = userOnProjectRepository.GetById(new int[] { entryDTO.UserId, entryDTO.ProjectId });
                if (userOnProject == null)
                {
                    throw new ValidationException("User is not assigned to this project");
                }
            }
            var entities = entries.ToList().ConvertAll(dto => new TimeSheetEntry(dto));
            var insertedEntities = timeSheetEntryRepository.AddRange(entities);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            timeSheetEntryRepository.Save();
            return result;
        }

        public void Update(DateTime date, IEnumerable<TimeSheetEntryDTO> entries)
        {
            foreach (TimeSheetEntryDTO entryDTO in entries)
            {
                var userOnProject = userOnProjectRepository.GetById(new int[] { entryDTO.UserId, entryDTO.ProjectId });
                if (userOnProject == null)
                {
                    throw new ValidationException("User is not assigned to this project");
                }
            }
            var entriesByDate = timeSheetEntryRepository.Search(x => x.Date == date);
            timeSheetEntryRepository.RemoveRange(entriesByDate);

            var entities = entries.ToList().ConvertAll(dto => new TimeSheetEntry(dto));
            timeSheetEntryRepository.AddRange(entities);
            timeSheetEntryRepository.Save();
        }

        public void Delete(int id)
        {
            var entry = timeSheetEntryRepository.GetById(id);
            if (entry == null)
            {
                throw new NotFoundException();
            }
            timeSheetEntryRepository.Delete(id);
            timeSheetEntryRepository.Save();
        }
    }
}
