using System.Collections.Generic;
using TimeSheet.DTO;

namespace TimeSheet.Contract
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetAll();
        ProjectDTO GetById(int id);
        ProjectDTO Insert(ProjectDTO projectDTO);
        void Update(int Id, ProjectDTO projectDTO);
        void Delete(int id);
    }
}
