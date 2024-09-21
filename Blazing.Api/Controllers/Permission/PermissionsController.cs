using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blazing.Domain.Entities;
using Blazing.Ecommerce.Data;
using Blazing.Ecommerce.Interface;

namespace Blazing.Api.Controllers.Permission
{
    [Route("api/[controller]")]
    public class PermissionsController(ILogger<PermissionsController> logger,
        IPermissionInfrastructureRepository permissionInfrastructureRepository) : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger = logger;
        private readonly IPermissionInfrastructureRepository _permissionInfrastructureRepository = permissionInfrastructureRepository;


        [HttpPost]
        public async Task<ActionResult> AddPermissions([FromBody] IEnumerable<PermissionDto> permissionDto,
            CancellationToken cancellationToken)
        {
            var result = await _permissionInfrastructureRepository.AddPermissions(permissionDto, cancellationToken);
            return Ok("Permissão cadastrada com sucesso.!");
        }
    }
}
