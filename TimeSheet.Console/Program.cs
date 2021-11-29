using System;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.DTO;
using TimeSheet.Entities;
using TimeSheet.Repository;
using TimeSheet.Repository.Repositories;
using TimeSheet.Service;

namespace TimeSheet.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeSheetDbContext context = new TimeSheetDbContext();
            RoleRepository roleRepository;
            CountryRepository countryRepository;
            CategoryRepository categoryRepository;
            ProjectRepository projectRepository;
            ClientRepository clientRepository;
            UserRepository userRepository;
            TeamLeaderRepository teamLeaderRepository;
            UserOnProjectRepository userOnProjectRepository;
            TimeSheetEntryRepository timeSheetEntryRepository;

            ClientService clientService;
            ProjectService projectService;
            CategoryService categoryService;
            CountryService countryService;
            RoleService roleService;
            TimeSheetEntryService timeSheetEntryService;
            UserService userService;

            //Role
            roleRepository = new RoleRepository(context);
            roleService = new RoleService(roleRepository);
            List<Role> roles = roleRepository.GetAll().ToList();
            RoleDTO role = new RoleDTO
            {
                Id = 8,
                Name = "Role23"
            };

            //roleService.Insert(role);
            //roleService.Update(3, role);
            //roleService.Delete(3);
            //roleService.Save();

            //Country
            countryRepository = new CountryRepository(context);
            countryService = new CountryService(countryRepository);
            List<Country> countries = countryRepository.GetAll().ToList();
            CountryDTO country = new CountryDTO
            {
                Id = 3,
                Name = "country3"
            };
            //countryService.Insert(country);
            //countryService.Update(3, country);
            //countryService.Delete(3);
            //countryService.Save();

            //Category
            categoryRepository = new CategoryRepository(context);
            categoryService = new CategoryService(categoryRepository);
            List<Category> categories = categoryRepository.GetAll().ToList();
            CategoryDTO category = new CategoryDTO
            {
                Id = 2,
                Name = "Back"
            };
            //categoryService.Insert(category);
            //categoryService.Update(2, category);
            //categoryService.Delete(2);
            //categoryService.Save();

            //Client
            clientRepository = new ClientRepository(context);
            clientService = new ClientService(clientRepository);
            List<ClientDTO> clients = clientService.GetAll().ToList();
            ClientDTO client = new ClientDTO
            {
                Id = 1,
                Name = "POWERWOLF",
                Address = "Client3 Street",
                City = "Client3 City",
                ZipCode = 21000,
                CountryId = 1
            };

            
            //clientService.Insert(client);
            //clientService.Update(4, client);
            //clientService.Delete(4);
            //clientService.Save();

            //Project
            projectRepository = new ProjectRepository(context);
            projectService = new ProjectService(projectRepository);
            List<Project> projects = projectRepository.GetAll().ToList();
            ProjectDTO project = new ProjectDTO
            {
                Id = 3,
                Name = "ProjectUpdatedAgain",
                Description = "Project6 description",
                ClientId = 3
            };
            //projectService.Insert(project);
            //projectService.Update(3, project);
            //projectService.Delete(3);
            //projectService.Save();

            //User
            userRepository = new UserRepository(context);
            userService = new UserService(userRepository);
            List<User> users = userRepository.GetAll().ToList();

            UserDTO user = new UserDTO
            {
                Name = "User4",
                Username = "user4",
                Email = "user4@gmail.com",
                Password = "333333",
                HoursPerWeek = 30,
                Status = true,
                RoleId = 1
            };
            //userService.Insert(user);
            //userService.Update(3, user);
            //userService.Delete(3);
            //userService.Save();

            //UserOnProject
            userOnProjectRepository = new UserOnProjectRepository(context);
            List<UserOnProject> usersOnProjects = userOnProjectRepository.GetAll().ToList();

            UserOnProject userOnProject = new UserOnProject
            {
                UserId = 5,
                ProjectId = 5
            };
            //userOnProjectRepository.Insert(userOnProject);
            //userOnProjectRepository.Update(3, userOnProject);
            //userOnProjectRepository.Delete(3);
            //userOnProjectRepository.Save();

            //TeamLeader
            teamLeaderRepository = new TeamLeaderRepository(context);
            List<TeamLeader> teamLeaders = teamLeaderRepository.GetAll().ToList();

            TeamLeader teamLeader = new TeamLeader
            {
                UserId = 5,
                ProjectId = 5
            };
            //teamLeaderRepository.Insert(teamLeader);
            //teamLeaderRepository.Update(3, teamLeader);
            //teamLeaderRepository.Delete(3);
            //teamLeaderRepository.Save();

            //TimeSheetEntry
            timeSheetEntryRepository = new TimeSheetEntryRepository(context);
            timeSheetEntryService = new TimeSheetEntryService(timeSheetEntryRepository);
            List<TimeSheetEntry> timeSheetEntries = timeSheetEntryRepository.GetAll().ToList();
  
            TimeSheetEntryDTO timeSheetEntry = new TimeSheetEntryDTO
            {
                UserId = 5,
                ProjectId = 5,
                CategoryId = 4,
                Description = "Description 5",
                Date = DateTime.Now,
                Time = 8,
                OverTime = 0
            };
            //timeSheetEntryService.Insert(timeSheetEntry);
            //timeSheetEntryService.Update(3, timeSheetEntry);
            //timeSheetEntryService.Delete(3);
            //timeSheetEntryService.Save();
        }
    }
}
