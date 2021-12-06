using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
    }
}
