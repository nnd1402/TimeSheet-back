using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Repository.Contract;

namespace TimeSheet.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = _categoryRepository.GetAll();
            var result = categories.ToList().ConvertAll(e => e.ConvertToDTO());
            _categoryRepository.Save();
            return result;
        }
    }
}
