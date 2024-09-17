using AutoMapper;
using BenchmarkDotNet.Disassemblers;
using Blazing.Identity.Dependencies;
using Blazing.Identity.Entities;
using Blazing.Identity.Dto;
using Blazing.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Blazing.Identity.RepositoryResult;

namespace Blazing.Identity.Repository
{
    public class RoleInfrastructureRepository(ILogger<RoleInfrastructureRepository> logger, 
        DependencyInjection dependencyInjection, UserManager<ApplicationUser> manager,RoleManager<ApplicationRole> roleManager)
        : IRoleInfrastructureRepository
    {
        private readonly ILogger<RoleInfrastructureRepository> _logger = logger;
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private readonly UserManager<ApplicationUser> _userManager = manager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public async Task<IEnumerable<IdentityResult>?> Add(IEnumerable<ApplicationRoleDto> roleDto, CancellationToken cancellationToken)
        {
            var roleList = roleDto.ToList();
            if (roleList.Count == 0)
                throw new ArgumentException("A lista de papéis está vazia.", nameof(roleDto));

            var result = new List<IdentityResult>();
            try
            {
                foreach (var item in roleList)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(item.Name);
                    if (roleExists)
                    {
                        _logger.LogInformation("A função {role} existe.", item.Name);
                        throw new ArgumentException("O funções do usuário ja existe.", nameof(item.Name));
                    }

                    var applicationRole = _dependencyInjection._mapper.Map<ApplicationRole>(item);

                    var resultRoleCreate = await _roleManager.CreateAsync(applicationRole);

                    result.Add(resultRoleCreate);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<IdentityResult>?> Update(IEnumerable<Guid> id, IEnumerable<ApplicationRoleDto> roleDto, CancellationToken cancellationToken)
        {
            var idList = id.ToList();
            if (idList.Count == 0 || idList.Any(i => i == Guid.Empty))
                throw new ArgumentException("O identificado da função não podem ficar vazios.", nameof(idList));
            
            var roleDtoList = roleDto.ToList();
            if (roleDtoList.Count == 0 || !roleDtoList.Any(i => idList.Contains(i.Id)))
                throw new ArgumentException("A lista de funções não pode ficar vazia.", nameof(roleDtoList));

            var result = new List<IdentityResult>();
            try
            {
                foreach (var item in roleDtoList)
                {
                    var role = await _roleManager.FindByIdAsync(item.Id.ToString());
                    if (role == null)
                    {
                        _logger.LogInformation("A função {role} não foi encontrada.", item.Name);
                        throw new ArgumentException("O funções do usuário não existe.", nameof(role.Name));
                    }
                    
                    var applicationRole = _dependencyInjection._mapper.Map(item, role);

                    var resultRoleUpdate = await _roleManager.UpdateAsync(applicationRole);

                    result.Add(resultRoleUpdate);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<IdentityResult>?> Delete(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idList = id.ToList();
            if (idList.Count == 0 || idList.Any(i => i == Guid.Empty))
            {
                _logger.LogInformation("O identificador da função não pode estar vazio. {nome}", nameof(idList));
                throw new ArgumentException("O identificador da função não pode estár vazio.", nameof(idList));
            }

            var result = new List<IdentityResult>();
            try
            {
                foreach (var itemId in idList)
                {
                    var role = await _roleManager.FindByIdAsync(itemId.ToString());
                    if (role == null)continue;

                    var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name.ToString());
                    foreach (var user in usersInRole)
                    {
                        var resultRemoveUserRole = await _userManager.RemoveFromRoleAsync(user, role.Name.ToString());
               
                        result.Add(resultRemoveUserRole);
                            
                    }

                    var resultRemoveRole = await _roleManager.DeleteAsync(role);

                    result.Add(resultRemoveRole);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<ApplicationRoleDto?>> GetById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var idsList = id.ToList();
            var applicationRoles = new List<ApplicationRole>();
            if (idsList.Count == 0 || idsList.Any(i => i == Guid.Empty))
            {
                _logger.LogInformation("O identificado da função não pode ficar vazio. {nome}", nameof(id));
                throw new ArgumentException("O identificado da função não pode ficar vazio.", nameof(id));
            }

            try
            {
                foreach (var itemId in idsList)
                {
                    var role = await _roleManager.FindByIdAsync(itemId.ToString());
                    if (role == null)
                    {
                        _logger.LogInformation("A função {role} não foi encontrada.", itemId);
                        throw new ArgumentException("A função {role} não foi encontrada.", nameof(role.Name));
                    }

                    applicationRoles.Add(role);
                }

                return _dependencyInjection._mapper.Map<IEnumerable<ApplicationRoleDto>>(applicationRoles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<ApplicationRoleDto?>> GetAll(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            try
            {
                var applicationRoles = await _dependencyInjection._appContext.Roles
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);
                if (!applicationRoles.Any())
                {
                    _logger.LogInformation("Nenhuma função foi encontrada.");
                    throw new ArgumentException("Nenhuma função foi encontrada.", nameof(applicationRoles));
                }
                
                var applicationRolesRto = _dependencyInjection._mapper.Map<IEnumerable<ApplicationRoleDto>>(applicationRoles);

                return applicationRolesRto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
