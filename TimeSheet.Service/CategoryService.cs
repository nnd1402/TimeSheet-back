using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class CategoryService
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Delete(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NullReferenceException();
            } else
            {
                categoryRepository.Delete(id);
                categoryRepository.Save();
            }
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            ICollection<CategoryDTO> result = new List<CategoryDTO>();
            var categories = categoryRepository.GetAll();

            foreach (Category category in categories)
            {
                var categoryDTO = Category.ConvertToDTO(category);
                result.Add(categoryDTO);
            }
            return result;
        }

        public CategoryDTO GetById(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                throw new NullReferenceException();
            }
            var categoryDTO = Category.ConvertToDTO(category);

            return categoryDTO;
        }

        public bool Insert(CategoryDTO categoryDTO)
        {
            bool status;
            try
            {
                var category = Category.ConvertFromDTO(categoryDTO);
                if (category == null)
                {
                    throw new NullReferenceException();
                }  else
                {
                    categoryRepository.Insert(category);
                    categoryRepository.Save();
                }
                status = true;
            }
            catch(Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, CategoryDTO categoryDTO)
        {
            bool status;
            try
            {
                var category = Category.ConvertFromDTO(categoryDTO);
                category = categoryRepository.GetById(id);
                if (id == 0 || category == null)
                {
                    throw new NullReferenceException();
                } else
                {
                    categoryRepository.Update(id, category);
                    categoryRepository.Save();
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
