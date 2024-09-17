using AutoMapper;
using Blazing.Identity.Dependency;
using Blazing.Identity.Entities;
using Blazing.Identity.Dto;
using Blazing.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blazing.Identity.Repository
{
    public class RoleInfrastructureRepository(IMemoryCache memoryCache,DependencyInjection dependencyInjection,RoleManager<ApplicationRole> roleManager) 
        : IRoleInfrastructureRepository
    {
        private readonly IMemoryCache _memoryCache = memoryCache;   
        private readonly DependencyInjection _dependencyInjection = dependencyInjection;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public async Task<IEnumerable<IdentityResult?>> Add(IEnumerable<ApplicationRoleDto> roleDto, CancellationToken cancellationToken)
        {
            if (roleDto == null || !roleDto.Any())
            {
                throw new ArgumentException("A lista de papéis está vazia.", nameof(roleDto));
            }

            var results = new List<IdentityResult>();

            foreach (var item in roleDto)
            {
                if (item == null)
                {
                    results.Add(IdentityResult.Failed(new IdentityError { Description = "Umas das funções fornecidos é nulo." }));
                    continue;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    results.Add(IdentityResult.Failed(new IdentityError { Description = "Operação cancelada." }));
                    break;
                }

                var roleExists = await _roleManager.RoleExistsAsync(item.Name);
                if (roleExists)
                {
                    results.Add(IdentityResult.Failed(new IdentityError { Description = $"O função '{item.Name}' já existe." }));
                    continue;
                }

                var applicationRole = _dependencyInjection._mapper.Map<ApplicationRole>(item);

                var result = await _roleManager.CreateAsync(applicationRole);
                UpdateCacheRoles(roleDto);
                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<IdentityResult?>> Update(IEnumerable<Guid> id, IEnumerable<ApplicationRoleDto> roleDto, CancellationToken cancellationToken)
        {
            if (roleDto == null)
            {
                throw new ArgumentException("O identificado da função não podem ficar vazios.");
            }

            var results = new List<IdentityResult>();
            foreach (var item in roleDto)
            {
                var role = await _roleManager.FindByIdAsync(item.Id.ToString());
                if (role == null)
                {
                    throw new InvalidOperationException("O papel do usuário não existe.");
                }

                var applicationRole = _dependencyInjection._mapper.Map(item, role);
               var result =  await _roleManager.UpdateAsync(applicationRole);
                results.Add(result);
                if (result.Succeeded)
                {
                    UpdateCacheRoles(roleDto);
                }
            }

            return results;
        }

        public Task<IEnumerable<IdentityResult?>> Delete(IEnumerable<Guid> id, IEnumerable<ApplicationRoleDto> obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationRoleDto?>> GetById(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<ApplicationRoleDto?>> GetAll(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            if (page <= 0)
            {
                throw new ArgumentException("A página deve ser maior que zero.", nameof(page));
            }

            if (pageSize > 50)
            {
                pageSize = 50;
            }
            var cacheKey = $"{page}-{pageSize}";
            try
            {
                if(!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ApplicationRole>? applicationRoles))
                {
                    applicationRoles = await _dependencyInjection._appContext.Roles
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

          
                    _memoryCache.Set(cacheKey, applicationRoles, TimeSpan.FromMinutes(5));
                }
                var applicationRolesRto = _dependencyInjection._mapper.Map<IEnumerable<ApplicationRoleDto>>(applicationRoles);
                var result = CacheMemoryManagerRoles(page, pageSize, applicationRolesRto);

                return result;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> ExistsAsync(bool boolean, bool booleanI, IEnumerable<ApplicationRoleDto> obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private static IEnumerable<ApplicationRoleDto> CacheMemoryManagerRoles(int page, int pageSize, IEnumerable<ApplicationRoleDto> role)
        {
            return role.Select(item => item with { Page = page, PageSize = pageSize });
        }


        /// <summary>
        /// Updates the cache with the provided roles. If the cache already contains roles for the given page and page size,
        /// it updates the existing roles with the provided ones. Otherwise, it adds the provided roles to the cache.
        /// </summary>
        /// <param name="roleDto">The roles to update or add to the cache.</param>
        private void UpdateCacheRoles(IEnumerable<ApplicationRoleDto> roleDto)
        {
            if (roleDto == null || !roleDto.Any())
                return;

            var page = roleDto.First().Page;
            var pageSize = roleDto.First().PageSize;
            var cacheKey = $"{page}-{pageSize}";
            try
            {
                if (_memoryCache.TryGetValue(cacheKey, out List<ApplicationRole>? cacheAppRole))
                {
                    var updatedRoles = roleDto.Select(dto => _dependencyInjection._mapper.Map<ApplicationRole>(dto)).ToList();

                    foreach (var updatedRole in updatedRoles)
                    {
                        var index = cacheAppRole.FindIndex(r => r.Id == updatedRole.Id);
                        if (index >= 0)
                        {
                            cacheAppRole[index] = updatedRole;
                        }
                        
                        else
                        {
                            cacheAppRole.Add(updatedRole);
                        }
                    }

                    _memoryCache.Set(cacheKey, cacheAppRole, TimeSpan.FromMinutes(5));
                }
                else
                {
                    var newCacheAppRole = roleDto.Select(dto => _dependencyInjection._mapper.Map<ApplicationRole>(dto)).ToList();
                    _memoryCache.Set(cacheKey, newCacheAppRole, TimeSpan.FromMinutes(5));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
