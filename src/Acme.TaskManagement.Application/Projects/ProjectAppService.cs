using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;

using Acme.TaskManagement.Domain.Entities;
using Acme.TaskManagement.Application.Contracts.Projects;
using Acme.TaskManagement.Contracts.Projects;

namespace Acme.TaskManagement.Application.Projects
{
    public class ProjectAppService : TaskManagementAppService, IProjectAppService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly ILogger<ProjectAppService> _logger;

        public ProjectAppService(
            IRepository<Project, Guid> projectRepository,
            ILogger<ProjectAppService> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        // ---------------------------------------
        // GET BY ID
        // ---------------------------------------
        [AllowAnonymous]
        public async Task<ProjectDto> GetAsync(Guid id)
        {
            try
            {
                var entity = await _projectRepository.GetAsync(id);
                return MapToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching project {Id}", id);
                throw new BusinessException("Could not fetch project.");
            }
        }

        // ---------------------------------------
        // GET LIST (Manual Pagination)
        // ---------------------------------------
        [AllowAnonymous]
        public async Task<PagedResultDto<ProjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                var queryable = await _projectRepository.GetQueryableAsync();

                var totalCount = queryable.Count();

                var items = queryable
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();

                return new PagedResultDto<ProjectDto>(
                    totalCount,
                    items.Select(MapToDto).ToList()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching project list");
                throw new BusinessException("Could not fetch project list.");
            }
        }

        // ---------------------------------------
        // CREATE
        // ---------------------------------------
        [Authorize]
        public async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input)
        {
            try
            {
                Validate(input);

                var entity = new Project(
                    Guid.NewGuid(),
                    input.Name,
                    input.Description
                );

                var saved = await _projectRepository.InsertAsync(entity);
                return MapToDto(saved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project.");
                throw new BusinessException("Could not create project.");
            }
        }

        // ---------------------------------------
        // UPDATE
        // ---------------------------------------
        [Authorize]
        public async Task<ProjectDto> UpdateAsync(Guid id, CreateUpdateProjectDto input)
        {
            try
            {
                Validate(input);

                var entity = await _projectRepository.GetAsync(id);

                entity.SetName(input.Name);
                entity.SetDescription(input.Description);

                var updated = await _projectRepository.UpdateAsync(entity);
                return MapToDto(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project.");
                throw new BusinessException("Could not update project.");
            }
        }

        // ---------------------------------------
        // DELETE
        // ---------------------------------------
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _projectRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project.");
                throw new BusinessException("Could not delete project.");
            }
        }

        // ---------------------------------------
        // APP SERVICE VALIDATION (simple)
        // ---------------------------------------
        private void Validate(CreateUpdateProjectDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new BusinessException("Name is required.");

            if (input.Name.Length > 100)
                throw new BusinessException("Name cannot exceed 100 characters.");
        }

        // ---------------------------------------
        // MANUAL MAPPER
        // ---------------------------------------
        private ProjectDto MapToDto(Project entity)
        {
            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreationTime = entity.CreationTime
            };
        }
    }
}
