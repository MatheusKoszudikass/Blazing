using Blazing.Identity.Dto;
using Blazing.Identity.Interface;
using Blazing.Identity.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blazing.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(ILogger<RoleController> logger,IRoleInfrastructureRepository roleInfrastructureRepository) : ControllerBase
    {
        private readonly ILogger<RoleController> _logger = logger;
        private readonly IRoleInfrastructureRepository _roleInfrastructureRepository = roleInfrastructureRepository;

        [HttpPost]
        public async Task<ActionResult> AddRole(IEnumerable<ApplicationRoleDto> roleDto,
            CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.Add(roleDto, cancellationToken);
            _logger.LogInformation("Funções adicionadas com sucesso. Total adicionado: {result}", result.Count());
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateRole(IEnumerable<ApplicationRoleDto> roleDto,
            CancellationToken cancellationToken)
        {
            var id = roleDto.Select(r => r.Id).AsEnumerable();
            var result = await _roleInfrastructureRepository.Update(id ,roleDto, cancellationToken);
            _logger.LogInformation("Funções atualizadas com sucesso. Total atualizado: {result}", result.Count());
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationRoleDto>>> GetAllRole(int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _roleInfrastructureRepository.GetAll(page, pageSize, cancellationToken);
            _logger.LogInformation("Funções recuperadas com sucesso. Total recuperado: {result}", result.Count());
            return Ok(result);
        }
    }
}
