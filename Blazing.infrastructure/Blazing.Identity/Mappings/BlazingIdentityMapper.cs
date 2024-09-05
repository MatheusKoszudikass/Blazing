using Blazing.Application.Dto;
using Blazing.Identity.Entities;

namespace Blazing.Identity.Mappings;

public class BlazingIdentityMapper
{
    public virtual async Task<IEnumerable<UserDto>> UserMapperAddedDto(IEnumerable<ApplicationUser> identityUser,
        IEnumerable<UserDto> existingUserDtos,
        CancellationToken cancellationToken)
    {
        // Cria uma lista para armazenar os resultados mapeados
        var result = new List<UserDto>();

        // Itera sobre a lista de identityUser e existingUserDtos ao mesmo tempo
        using (var identityUserEnumerator = identityUser.GetEnumerator())
        using (var existingUserDtosEnumerator = existingUserDtos.GetEnumerator())
        {
            while (identityUserEnumerator.MoveNext() && existingUserDtosEnumerator.MoveNext())
            {
                var identity = identityUserEnumerator.Current;
                var existingDto = existingUserDtosEnumerator.Current;

                // Mapeia o Id do ApplicationUser para o UserDto existente
                existingDto.Id = identity.Id;
                existingDto.PasswordHash = identity.PasswordHash;

                // Adiciona o UserDto à lista de resultados
                result.Add(existingDto);
            }
        }

        await Task.CompletedTask;
        return result;
    }


    public virtual async Task<List<ApplicationUser>> UserMapperAddedApp(IEnumerable<UserDto> userDto,
        CancellationToken cancellationToken)
    {
        var result = userDto.Select(u => new ApplicationUser
        {
            Id = u.Id,
            Status = u.Status,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            PhoneNumber = u.PhoneNumber
        });

        await Task.CompletedTask;
        return result.ToList();
    }

    public virtual async Task<IEnumerable<UserDto>> UserMapperUpdateDto(IEnumerable<UserDto> userDto,
        CancellationToken token)
    {
        var result = userDto.Select(u => new UserDto
        {
            Id = u.Id,
            Status = u.Status,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber
        });

        await Task.CompletedTask;
        return result;
    }

    public virtual async Task<IEnumerable<UserDto>> UserMapperDtoGet(IEnumerable<ApplicationUser> identityUsers,
        CancellationToken cancellationToken)
    {
        var result = identityUsers.Select(i => new UserDto
        {
            Id = i.Id,
            Status = i.Status,
            FirstName = i.FirstName,
            LastName = i.LastName,
            UserName = i.UserName,
            Email = i.Email,
            PhoneNumber = i.PhoneNumber
            
        });

        await Task.CompletedTask;
        return result;
    }

    public virtual async Task<IEnumerable<ApplicationUser>> UserMapperApplicationUser(IEnumerable<UserDto> userDto,
        CancellationToken cancellationToken)
    {
        var result = userDto.Select(u => new ApplicationUser
        {
            Status = u.Status,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            PhoneNumber = u.PhoneNumber,
            CreationDate = u.DataCreated,
            LastUpdate = DateTime.Now,
        });

        await Task.CompletedTask;
        return result;
    }

    public virtual async Task<IEnumerable<UserDto>> UserMapperDto(IEnumerable<ApplicationUser?> identityUser,
        CancellationToken cancellationToken)
    {
        var result = identityUser.Select(i => new UserDto
        {
            Id = i.Id,
            Status = i.Status,
            FirstName = i.FirstName,
            LastName = i.LastName,
            UserName = i.UserName,
            Email = i.Email,
            PasswordHash = i.PasswordHash,
            PhoneNumber = i.PhoneNumber,
            DataCreated = i.CreationDate,
            DataUpdated = i.LastUpdate
        });

        await Task.CompletedTask;
        return result;
    }
}