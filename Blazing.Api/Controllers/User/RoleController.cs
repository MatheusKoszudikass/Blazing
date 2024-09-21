using Blazing.Identity.Dto;
using Blazing.Identity.Interface;
using Blazing.Identity.Repository;
using Blazing.Identity.RepositoryResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blazing.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(ILogger<RoleController> logger,IRoleInfrastructureRepository roleInfrastructureRepository) : ControllerBase
    {
        private readonly ILogger<RoleController> _logger = logger;
        private readonly IRoleInfrastructureRepository _roleInfrastructureRepository = roleInfrastructureRepository;

        /// <summary>
        /// Adds new roles to the system.
        /// </summary>
        /// <param name="roleDto">A list of DTO objects containing information about the new roles.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Returns the result of the role addition as an enumerable of IdentityResult.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<IdentityResult>>> AddRole(IEnumerable<ApplicationRoleDto> roleDto,
            CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.Add(roleDto, cancellationToken);
            _logger.LogInformation("Funções adicionadas com sucesso. Total: {result}", 
                result);

            return Ok(result);
        }

        /// <summary>
        /// Updates existing roles in the system.
        /// </summary>
        /// <param name="id">A list of GUIDs representing the roles to be updated.</param>
        /// <param name="roleDto">A list of DTO objects with updated role information.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Returns the result of the role update as an enumerable of IdentityResult.</returns>
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<IdentityResult>>> UpdateRole([FromQuery]IEnumerable<Guid>id, IEnumerable<ApplicationRoleDto> roleDto,
            CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.Update(id ,roleDto, cancellationToken);
            _logger.LogInformation("Funções atualizadas com sucesso. {result}.",
                result);

            return Ok(result);
        }

        /// <summary>
        /// Deletes roles from the system.
        /// </summary>
        /// <param name="id">A list of GUIDs representing the roles to be deleted.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Returns the result of the role deletion as an enumerable of IdentityResult.</returns>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<IEnumerable<IdentityResult>>> DeleteRole([FromQuery]IEnumerable<Guid> id,
            CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.Delete(id, cancellationToken);
            _logger.LogInformation("Funções excluídas com sucesso. Total {result}",
                result);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a role by its identifier.
        /// </summary>
        /// <param name="id">A list of GUIDs representing the roles to be retrieved.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Returns the DTO of the retrieved role.</returns>
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<ApplicationRoleDto>> GetRoleById([FromQuery] IEnumerable<Guid> id,
            CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.GetById(id, cancellationToken);
            _logger.LogInformation("Função recuperada com sucesso. Total recuperado: {result}", result.Count());

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all roles with pagination.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of roles to retrieve per page.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>Returns a paginated list of all roles as DTOs.</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationRoleDto>>> GetAllRole(int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.GetAll(page, pageSize, cancellationToken);
            _logger.LogInformation("Funções recuperadas com sucesso. Total recuperado: {result}", result.Count());

            return Ok(result);
        }
    }
}
