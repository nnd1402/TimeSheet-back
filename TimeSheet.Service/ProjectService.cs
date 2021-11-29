using System;
using System.Collections.Generic;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Repositories;

namespace TimeSheet.Service
{
    public class ProjectService
    {
        private readonly ProjectRepository projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public void Delete(int id)
        {
            var project = projectRepository.GetById(id);
            if (project == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                projectRepository.Delete(id);
                projectRepository.Save();
            }
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            ICollection<ProjectDTO> result = new List<ProjectDTO>();
            IEnumerable<Project> projects = projectRepository.GetAll();

            foreach (Project project in projects)
            {
                var projectDTO = Project.ConvertToDTO(project);
                result.Add(projectDTO);
            }
            return result;
        }

        public ProjectDTO GetById(int id)
        {
            var project = projectRepository.GetById(id);
            if (project == null)
            {
                throw new NullReferenceException();
            }
            var projectDTO = Project.ConvertToDTO(project);

            return projectDTO;
        }

        public bool Insert(ProjectDTO projectDTO)
        {
            bool status;
            try
            {
                var project = Project.ConvertFromDTO(projectDTO);
                if (project == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    projectRepository.Insert(project);
                    projectRepository.Save();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool Update(int id, ProjectDTO projectDTO)
        {
            bool status;
            try
            {
                var project = Project.ConvertFromDTO(projectDTO);
                project = projectRepository.GetById(id);
                if (id == 0 || project == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    projectRepository.Update(id, project);
                    projectRepository.Save();
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
