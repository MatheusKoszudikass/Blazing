using AutoMapper;
using Blazing.Application.Dto;
using Blazing.Application.Interface.User;
using Blazing.Domain.Entities;
using Blazing.Domain.Interface.Services.User;

namespace Blazing.Application.Services
{
    #region Application user service.
    public class UserAppService(IMapper mapper, IUserDomainService userDomainService) : IUserAppService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserDomainService _userCrudDomainService = userDomainService;

        /// <summary>
        /// Adds a list of userDto to the domain.
        /// </summary>
        /// <param name="userDto">The list of userDto to be added.</param>
        /// <returns>The list of userDto that have been added.</returns>
        public async Task<IEnumerable<UserDto>> AddUsers(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDto);

            var userResult = await _userCrudDomainService.Add(users, cancellationToken);

            var userDtoResult = _mapper.Map<IEnumerable<UserDto>>(userResult);

            return userDtoResult;
        }

        /// <summary>
        /// Updates an existing userDto based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the userDto to update.</param>
        /// <param name="usersDto">The userDto object containing the updated data.</param>
        /// <param name="usersDtoUpdate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The updated userDto, if found.</returns>
        public async Task<IEnumerable<UserDto>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto,
            IEnumerable<UserDto?> usersDtoUpdate, CancellationToken cancellationToken)
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
        /// <param name="usersDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of UserDto that were deleted.</returns>
        public async Task<IEnumerable<UserDto>> DeleteUsers(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto,
            CancellationToken cancellationToken)
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
        /// <param name="usersDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The UserDto corresponding to the given ID.</returns>
        public async Task<IEnumerable<UserDto>> GetUserById(IEnumerable<Guid> id, IEnumerable<UserDto> usersDto, CancellationToken cancellationToken)
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
        public async Task<IEnumerable<UserDto>> GetAllUsers(IEnumerable<UserDto> usersDto, CancellationToken cancellationToken)
        {
             var users = _mapper.Map<IEnumerable<User>>(usersDto);

             users = await _userCrudDomainService.GetAll(users, cancellationToken);

             usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }

        /// <summary>
        /// Checks if UserDto exist based on the provided flag.
        /// </summary>
        /// <param name="id">A boolean flag indicating the existence check.</param>
        /// <param name="existsName"></param>
        /// <param name="existsEmail"></param>
        /// <param name="userDto"></param>
        /// <param name="cancellationToken"></param>
        public async Task<bool?> ExistsUsers(bool id, bool existsName, bool existsEmail, IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IEnumerable<User>>(userDto);

            var usersResult = await _userCrudDomainService.UserExistsAsync(id, existsName, existsEmail, users, cancellationToken);

            return usersResult;
        }
    }
    #endregion
}
