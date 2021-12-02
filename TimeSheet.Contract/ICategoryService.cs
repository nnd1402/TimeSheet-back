using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        CategoryDTO Insert(CategoryDTO categoryDTO);
        void Update(int Id, CategoryDTO categoryDTO);
        void Delete(int id);
    }
}
