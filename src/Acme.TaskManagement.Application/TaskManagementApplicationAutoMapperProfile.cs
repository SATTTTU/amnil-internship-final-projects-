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

            CreateMap<TaskItem, TaskDto>();
            CreateMap<CreateUpdateTaskDto, TaskItem>();

            CreateMap<TaskStatus, TaskStatusDto>().ReverseMap();
        }
    }
}
