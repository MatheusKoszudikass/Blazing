using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interfaces.User;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Services;

namespace Blazing.Application.Services
{
    #region Application user service.
    public class UserAppService(IMapper mapper, ICrudDomainService<User> userDomainService) : IUserAppService<UserDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICrudDomainService<User> _userCrudDomainService = userDomainService;

        /// <summary>
        /// Adds a list of userDto to the domain.
        /// </summary>
        /// <param name="userDto">The list of userDto to be added.</param>
        /// <returns>The list of userDto that have been added.</returns>
        public async Task<IEnumerable<UserDto?>> AddUsers(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDto);

            var userResut = await _userCrudDomainService.Add(users, cancellationToken);

            var userDtoResult = _mapper.Map<IEnumerable<UserDto>>(userResut);

            return userDtoResult;
        }

        /// <summary>
        /// Updates an existing userDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the userDto to update.</param>
        /// <param name="usersDto">The userDto object containing the updated data.</param>
        /// <returns>The updated userDto, if found.</returns>
        public async Task<IEnumerable<UserDto?>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto, IEnumerable<UserDto> usersDtoUpdate, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(usersDto);
            var userUpdate = _mapper.Map<IEnumerable<User>>(usersDtoUpdate);

            var usersResultDto = await _userCrudDomainService.Update(id, users, userUpdate, cancellationToken);

            usersDto = _mapper.Map<IEnumerable<UserDto>>(usersResultDto);

            return usersDto;
        }

        /// <summary>
        /// Deletes UserDto based on a list of provided IDs.
        /// </summary>
        /// <param name="id">The list of UserDto IDs to be deleted.</param>
        /// <returns>The list of UserDto that were deleted.</returns>
        public async Task<IEnumerable<UserDto?>> DeleteUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(usersDto);

            var usersResult = await _userCrudDomainService.Delete(id, users, cancellationToken);

            usersDto = _mapper.Map<IEnumerable<UserDto>>(usersResult);

            return usersDto;
        }

        /// <summary>
        /// Gets a specific UserDto based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the UserDto to get.</param>
        /// <returns>The UserDto corresponding to the given ID.</returns>
        public async Task<IEnumerable<UserDto?>> GetUserById(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(usersDto);

            var usersResult = await _userCrudDomainService.GetById(id, users, cancellationToken);

            usersDto = _mapper.Map<IEnumerable<UserDto>>(usersResult);

            return usersDto;
        }

        /// <summary>
        /// Gets all UserDto from the domain.
        /// </summary>
        /// <returns>The list of all UserDto.</returns>
        public async Task<IEnumerable<UserDto?>> GetAllUsers(IEnumerable<UserDto> usersDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(usersDto);

            await _userCrudDomainService.GetAll(users, cancellationToken);

            return usersDto;
        }

        /// <summary>
        /// Checks if UserDto exist based on the provided flag.
        /// </summary>
        /// <param name="nameExists">A boolean flag indicating the existence check.</param>
        public async Task<bool?> ExistsUsers(bool id, bool existsName, bool existsEmail, IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDto);

            var usersResult = await _userCrudDomainService.ExistsAsync(id, existsName, users, cancellationToken);

            return await Task.FromResult(usersResult);
        }
    }
    #endregion
}
