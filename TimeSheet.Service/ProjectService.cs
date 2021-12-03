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
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var projects = projectRepository.GetAll();
            var result = projects.ToList().ConvertAll(e => e.ConvertToDTO());
            projectRepository.Save();
            return result;
        }
        public ProjectDTO GetById(int id)
        {
            var project = projectRepository.GetById(id);
            if (project == null)
            {
                throw new NotFoundException();
            }
            var projectDTO = project.ConvertToDTO();

            return projectDTO;
        }
        public ProjectDTO Insert(ProjectDTO projectDTO)
        {
            if (string.IsNullOrEmpty(projectDTO.Description))
            {
                throw new ValidationException("Description cannot be empty");
            }
            if (string.IsNullOrEmpty(projectDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }

            var dtoToEntity = new Project(projectDTO);
            var dbProject = projectRepository.Insert(dtoToEntity);
            projectRepository.Save();
            var result = dbProject.ConvertToDTO();
            return result;
        }
        public void Update(int id, ProjectDTO projectDTO)
        {
            if (string.IsNullOrEmpty(projectDTO.Description))
            {
                throw new ValidationException("Description cannot be empty");
            }
            if (string.IsNullOrEmpty(projectDTO.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            var dtoToEntity = new Project(projectDTO);
            projectRepository.Update(id, dtoToEntity);
            projectRepository.Save();
        }
        public void Delete(int id)
        {
            var project = projectRepository.GetById(id);
            if (project == null)
            {
                throw new NotFoundException();
            }
            projectRepository.Delete(id);
            projectRepository.Save();
        }
    }
}
