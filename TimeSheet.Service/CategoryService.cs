using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

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
        public CategoryDTO GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException();
            }
            var categoryDTO = category.ConvertToDTO();

            return categoryDTO;
        }
        public CategoryDTO Insert(CategoryDTO categoryDTO)
        {
            if (string.IsNullOrEmpty(categoryDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Category(categoryDTO);
            var dbCategory = _categoryRepository.Insert(dtoToEntity);
            _categoryRepository.Save();
            var result = dbCategory.ConvertToDTO();
            return result;
        }
        public void Update(int id, CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ValidationException("Category must be provided");
            }
            if (string.IsNullOrEmpty(categoryDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Category(categoryDTO);
            _categoryRepository.Update(id, dtoToEntity);
            _categoryRepository.Save();
        }
        public void Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException();
            }
            _categoryRepository.Delete(id);
            _categoryRepository.Save();
        }
    }
}
