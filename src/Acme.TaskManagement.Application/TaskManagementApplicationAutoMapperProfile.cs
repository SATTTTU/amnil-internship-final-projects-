using AutoMapper;
using Acme.TaskManagement.Domain.Entities;
using Acme.TaskManagement.Domain.Shared.Enums;
using Acme.TaskManagement.Contracts.Projects;
using Acme.TaskManagement.Contracts.Tasks;

namespace Acme.TaskManagement
{
    public class TaskManagementApplicationAutoMapperProfile : Profile
    {
        public TaskManagementApplicationAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateUpdateProjectDto, Project>();

            CreateMap<Task, TaskDto>();
            CreateMap<CreateUpdateTaskDto, Task>();

            CreateMap<TaskStatus, TaskStatusDto>().ReverseMap();
        }
    }
}
