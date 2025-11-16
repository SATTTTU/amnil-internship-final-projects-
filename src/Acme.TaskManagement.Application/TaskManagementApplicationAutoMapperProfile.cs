using AutoMapper;
using Acme.TaskManagement.Domain.Entities;
using Acme.TaskManagement.Domain.Shared.Enums;


namespace Acme.TaskManagement;

public class TaskManagementApplicationAutoMapperProfile : Profile
{
    public TaskManagementApplicationAutoMapperProfile()
    {
       
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateUpdateProjectDto, Project>();

            CreateMap<Task, TaskDto>();
            CreateMap<CreateUpdateTaskDto, Task>();
            CreateMap<TaskStatus, TaskStatusDto>().ReverseMap();
        
    

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
