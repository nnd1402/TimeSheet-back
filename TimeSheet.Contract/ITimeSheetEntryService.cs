using System;
using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface ITimeSheetEntryService
    {
        IEnumerable<TimeSheetEntryDTO> GetAll();
        TimeSheetEntryDTO GetById(int id);
        IEnumerable<TimeSheetEntryDTO> InsertMany(IEnumerable<TimeSheetEntryDTO> entries);
        void Update(DateTime date, IEnumerable<TimeSheetEntryDTO> entries);
        void Delete(int id);
    }
}
