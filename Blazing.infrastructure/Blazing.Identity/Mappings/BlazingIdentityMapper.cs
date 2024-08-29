using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Identity.Entities;

namespace Blazing.Identity.Mappings
{
    public class BlazingIdentityMapper
    {
        public virtual async Task<IEnumerable<ApplicationUser>> UserMapperApplicationUser(IEnumerable<UserDto> userDto)
        {
            var result = userDto.Select(u => new ApplicationUser
            {
                Status = u.Status,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PhoneNumber = u.PhoneNumber
                
            });

            return await Task.FromResult(result);
        }

        public virtual async Task<IEnumerable<UserDto>> UserMapperUserDto(IEnumerable<ApplicationUser> identityUser)
        {
            var result = identityUser.Select(i => new UserDto
            {
                IdentityId = i.Id,
                Status = i.Status,
                FirstName = i.FirstName,
                LastName = i.LastName,
                UserName = i.UserName,
                Email = i.Email,
                PasswordHash = i.PasswordHash,
                PhoneNumber = i.PhoneNumber
            });

            return await Task.FromResult(result);
        }

        public virtual async Task<IEnumerable<UserDto>> UserMapperUserDtoGet(IEnumerable<ApplicationUser> identityUsers)
        {
            var result = identityUsers.Select(i => new UserDto
            {
                Status = i.Status,
                FirstName = i.FirstName,
                LastName = i.LastName,
                UserName = i.UserName,
                Email = i.Email,
                PhoneNumber = i.PhoneNumber
            });

            return await Task.FromResult(result);
        }
    }
}
