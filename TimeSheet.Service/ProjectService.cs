using System.Collections.Generic;
using System.Linq;
using TimeSheet.Contract;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository.Contract;
using TimeSheet.Service.Exceptions;

namespace TimeSheet.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserOnProjectRepository _userOnProjectRepository;
        private readonly ITeamLeaderRepository _teamLeaderRepository;

        public ProjectService(IProjectRepository projectRepository, IClientRepository clientRepository, IUserOnProjectRepository userOnProjectRepository, ITeamLeaderRepository teamLeaderRepository)
        {
            this._projectRepository = projectRepository;
            this._clientRepository = clientRepository;
            this._userOnProjectRepository = userOnProjectRepository;
            this._teamLeaderRepository = teamLeaderRepository;
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var projects = _projectRepository.GetAll();

            var result = projects.ToList().ConvertAll(p =>
            {
                var lead = _teamLeaderRepository.Search(l => l.ProjectId == p.Id).Single();
                var usersOnProject = _userOnProjectRepository.Search(uop => uop.ProjectId == p.Id);
                var projectDTO = p.ConvertToDTO(lead, usersOnProject);
                return projectDTO;
            });
            _projectRepository.Save();
            return result;
        }
        public ProjectDTO GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
            {
                throw new NotFoundException();
            }
            var lead = _teamLeaderRepository.Search(l => l.ProjectId == project.Id).Single();
            var usersOnProject = _userOnProjectRepository.Search(uop => uop.ProjectId == project.Id);
            var projectDTO = project.ConvertToDTO(lead, usersOnProject);

            return projectDTO;
        }

        public ProjectDTO GetByName(string name)
        {
            var lead = _projectRepository.Search(l => l.Name == name);
            return null;
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
            var client = _clientRepository.GetById(projectDTO.ClientId);
            if (client == null)
            {
                throw new ValidationException("Client does not exist");
            }
            var projectByName = _projectRepository.Search(l => l.Name == projectDTO.Name);
            if(projectByName.Count() > 0)
            {
                throw new ValidationException("Project already exists");
            }
            var dtoToEntity = new Project(projectDTO);
            var dbProject = _projectRepository.Insert(dtoToEntity);
            //sad mora save da dbProject.Id ne bude nula
            _projectRepository.Save();
           
            var usersOnProject = projectDTO.UserIds.ToList().ConvertAll(uId => new UserOnProject(uId, dbProject.Id));
            //ovde uspesno hvata ideve, ali User i Project budu null
            var dbUsersOnProject = _userOnProjectRepository.AddRange(usersOnProject);
            _userOnProjectRepository.Save();


            var teamLeader = new TeamLeader(projectDTO.LeadUserId, dbProject.Id);
            
            //ovde uspesno hvata ideve, UserOnProject bude null
            var dbTeamLeader = _teamLeaderRepository.Insert(teamLeader);
            _teamLeaderRepository.Save();
            var result = dbProject.ConvertToDTO(dbTeamLeader, dbUsersOnProject);
            _projectRepository.Save();
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
            var client = _clientRepository.GetById(projectDTO.ClientId);
            if (client == null)
            {
                throw new ValidationException("Client does not exist");
            }
            var dtoToEntity = new Project(projectDTO);
            _projectRepository.Update(id, dtoToEntity);
            var teamLeader = new TeamLeader(projectDTO.LeadUserId, dtoToEntity.Id);
            var existingLeader = _teamLeaderRepository.Search(l => l.ProjectId == dtoToEntity.Id).Single();
            _teamLeaderRepository.Delete(existingLeader.UserId, existingLeader.ProjectId );
            var dbTeamLeader = _teamLeaderRepository.Insert(teamLeader);
            var existingUsersOnProject = _userOnProjectRepository.Search(uop => uop.ProjectId == dtoToEntity.Id);
            _userOnProjectRepository.RemoveRange(existingUsersOnProject);
            var usersOnProject = projectDTO.UserIds.ToList().ConvertAll(uId => new UserOnProject(uId, dtoToEntity.Id));
            _userOnProjectRepository.AddRange(existingUsersOnProject);
            _projectRepository.Save();
        }
        public void Delete(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project == null)
            {
                throw new NotFoundException();
            }
            _projectRepository.Delete(id);
            _projectRepository.Save();
        }
    }
}
