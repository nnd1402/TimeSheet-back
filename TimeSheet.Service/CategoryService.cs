using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = categoryRepository.GetAll();
            var insertedEntities = categoryRepository.AddRange(categories);
            var result = insertedEntities.ToList().ConvertAll(e => e.ConvertToDTO());
            categoryRepository.Save();
            return result;
        }
        public CategoryDTO GetById(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException();
            }
            var categoryDTO = category.ConvertToDTO();

            return categoryDTO;
        }
        public CategoryDTO Insert(CategoryDTO categoryDTO)
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
            var dbCategory = categoryRepository.Insert(dtoToEntity);
            categoryRepository.Save();
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
            categoryRepository.Update(id, dtoToEntity);
            categoryRepository.Save();
        }
        public void Delete(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NotFoundException();
            }
            categoryRepository.Delete(id);
            categoryRepository.Save();
        }
    }
}
