using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Service
{
    public class TimeSheetEntryService : ITimeSheetEntryService
    {
        private readonly ITimeSheetEntryRepository _timeSheetEntryRepository;
        private readonly IUserOnProjectRepository _userOnProjectRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TimeSheetEntryService(ITimeSheetEntryRepository timeSheetEntryRepository, IUserOnProjectRepository userOnProjectRepository, ICategoryRepository categoryRepository)
        {
            this._timeSheetEntryRepository = timeSheetEntryRepository;
            this._userOnProjectRepository = userOnProjectRepository;
            this._categoryRepository = categoryRepository;
        }

        public IEnumerable<TimeSheetEntryDTO> GetAll()
        {
            var entries = _timeSheetEntryRepository.GetAll();
            var result = entries.ToList().ConvertAll(e => e.ConvertToDTO());
            _timeSheetEntryRepository.Save();
            return result;
        }

        public TimeSheetEntryDTO GetById(int id)
        {
            var entry = _timeSheetEntryRepository.GetById(id);
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
                var userOnProject = _userOnProjectRepository.GetById( entryDTO.UserId, entryDTO.ProjectId );
                if (userOnProject == null)
                {
                    throw new ValidationException("User is not assigned to this project");
                }
                var category = _categoryRepository.GetById(entryDTO.CategoryId);
                if (category == null)
                {
                    throw new ValidationException("Category does not exist");
                }
            }
            var entities = entries.ToList().ConvertAll(dto => new TimeSheetEntry(dto));
            var insertedEntities = _timeSheetEntryRepository.AddRange(entities);
            _timeSheetEntryRepository.Save();
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            return result;
        }

        public void Update(DateTime date, IEnumerable<TimeSheetEntryDTO> entries)
        {
            foreach (TimeSheetEntryDTO entryDTO in entries)
            {
                var userOnProject = _userOnProjectRepository.GetById(entryDTO.UserId, entryDTO.ProjectId);
                if (userOnProject == null)
                {
                    throw new ValidationException("User is not assigned to this project");
                }
            }
            var entriesByDate = _timeSheetEntryRepository.Search(x => x.Date == date);
            _timeSheetEntryRepository.RemoveRange(entriesByDate);
            _timeSheetEntryRepository.Save();
            var entities = entries.ToList().ConvertAll(dto => new TimeSheetEntry(dto));
            _timeSheetEntryRepository.AddRange(entities);
            _timeSheetEntryRepository.Save();
        }

        public void Delete(int id)
        {
            var entry = _timeSheetEntryRepository.GetById(id);
            if (entry == null)
            {
                throw new NotFoundException();
            }
            _timeSheetEntryRepository.Delete(id);
            _timeSheetEntryRepository.Save();
        }

        public IEnumerable<TimeSheetEntryDTO> GetByDate(DateTime date)
        {
            var entriesByDate = _timeSheetEntryRepository.Search(x => x.Date == date);
            var result = entriesByDate.ToList().ConvertAll(e => e.ConvertToDTO());
            return result;
        }
    }
}
