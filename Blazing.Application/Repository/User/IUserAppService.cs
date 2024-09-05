using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Application.Interfaces.User
{
    public interface IUserAppService<T> where T : BaseEntityDto
    {
        /// <summary>
        /// Adds a collection of userDto.
        /// </summary>
        /// <param name="userDto">A collection of userDto to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the added userDto.</returns>
        Task<IEnumerable<UserDto?>> AddUsers(IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a userDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the userDto to be updated.</param>
        /// <param name="userDto">The updated userDto details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated userDto.</returns>
        Task<IEnumerable<UserDto?>> UpdateUsers(IEnumerable<Guid> id, IEnumerable<UserDto> userDto, IEnumerable<UserDto> productDtosUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of userDto by their IDs.
        /// </summary>
        /// <param name="id">A collection of userDto IDs to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a collection of the deleted userDto.</returns>
        Task<IEnumerable<UserDto?>> DeleteUsers(IEnumerable<Guid> id, IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a userDto by its ID.
        /// </summary>
        /// <param name="id">The ID of the userDto to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with the userDto details.</returns>
        Task<IEnumerable<UserDto?>> GetUserById(IEnumerable<Guid> id, IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all userDto.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a collection of all userDto.</returns>
        Task<IEnumerable<UserDto?>> GetAllUsers(IEnumerable<UserDto> userDto, CancellationToken cancellationToken);

        /// <summary>
        ///  checks the name to see if it already exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool?> ExistsUsers(bool id, bool existsName, bool existsEmail, IEnumerable<UserDto> productDto, CancellationToken cancellationToken);
    }
}
