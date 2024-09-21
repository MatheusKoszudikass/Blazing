using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Permission;
using Blazing.Domain.Interface.Services;
// ReSharper disable All

namespace Blazing.Domain.Services
{
    public class PermissionDomainService : ICrudDomainService<Permission>
    {

        /// <summary>
        /// Adds a collection of permissions by setting the creation timestamp and clearing the update and deletion timestamps. 
        /// Validates the permissions before adding them to the IEnumerable.
        /// </summary>
        /// <param name="permissions">Collection of permissions to add.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>A collection of added permissions with timestamps set for creation.</returns>
        /// <exception cref="DomainException">Thrown if validation fails.</exception>
        public async Task<IEnumerable<Permission>> Add(IEnumerable<Permission> permissions, CancellationToken cancellationToken)
        {
            var permissionsList = await ConvertIEnumerableToList(permissions);
            ValidatePermission(permissionsList);

            try
            {
                foreach (var itemPermission in permissionsList)
                {
                    itemPermission.DataCreated = DateTime.Now;
                    itemPermission.UserUpdated = Guid.Empty;
                    itemPermission.Roles = new List<Role>();
                    itemPermission.DataUpdated = null;
                    itemPermission.DataDeleted = null;
                }

                await Task.CompletedTask;
                return permissionsList;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a collection of permissions based on the provided IDs and updated permission data. 
        /// It performs validation on the IDs, permissions, and updates, applies the changes, and returns the updated permissions.
        /// </summary>
        /// <param name="id">A collection of permission IDs to update.</param>
        /// <param name="permissions">The original collection of permissions.</param>
        /// <param name="permissionsUpdate">The updated collection of permissions.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>An updated collection of permissions.</returns>
        /// <exception cref="DomainException">Thrown if validation or update fails.</exception>
        public async Task<IEnumerable<Permission>> Update(IEnumerable<Guid> id, 
            IEnumerable<Permission> permissions, IEnumerable<Permission> permissionsUpdate, CancellationToken cancellationToken)
        {
            try
            {
                ValidateIds(id);
                ValidatorPermissionWithId(id, permissions);
                ValidatePermissionsUpdate(id, permissionsUpdate);

                var permissionsList = await ConvertIEnumerableToList(permissions);
                var updatePermission = await ConvertIEnumerableToList(permissionsUpdate);

                var result  = ApplyUpdates(permissionsList, updatePermission);
                ValidatePermissionsApplyUpdate(result);

                await Task.CompletedTask;
                return result;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves all permissions from the provided IEnumerable. 
        /// Validates that the IEnumerable contains permissions before returning it.
        /// </summary>
        /// <param name="permissions">IEnumerable of all permissions to retrieve.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>A collection of permissions.</returns>
        /// <exception cref="DomainException">Thrown if validation fails.</exception>
        public async Task<IEnumerable<Permission>> GetAll(IEnumerable<Permission> permissions,
            CancellationToken cancellationToken)
        {
            try
            {
                var permissionsList = await ConvertIEnumerableToList(permissions);
                ValidatePermission(permissions);

                await Task.CompletedTask;
                return permissionsList;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a collection of permissions based on the provided IDs. 
        /// Validates the IDs and ensures they exist in the provided permissions IEnumerable. 
        /// Returns the IEnumerable of permissions if validation is successful.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the permission IDs to retrieve.</param>
        /// <param name="permissions">IEnumerable of all permissions to validate against.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>A collection of permissions matching the provided IDs.</returns>
        /// <exception cref="DomainException">Thrown if validation fails.</exception>
        public async Task<IEnumerable<Permission>> GetById(IEnumerable<Guid> id,
            IEnumerable<Permission> permissions, CancellationToken cancellationToken)
        {
            try
            {
                ValidateIds(id);
                ValidatorPermissionWithId(id, permissions);

                await Task.CompletedTask;
                return permissions;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Marks a collection of permissions as deleted based on the provided IDs. 
        /// Validates the IDs and updates the `DataDeleted` property for each permission. 
        /// Returns the updated IEnumerable of permissions with deletion timestamps set.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to delete.</param>
        /// <param name="permissions">IEnumerable of all permissions to update.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>A collection of updated permissions with deletion timestamps.</returns>
        /// <exception cref="DomainException">Thrown if validation fails.</exception>
        public async Task<IEnumerable<Permission>> Delete(IEnumerable<Guid> id, IEnumerable<Permission> permissions, CancellationToken cancellationToken)
        {
            try
            {
                var idList = id.ToList();
                ValidateIds(idList);

                var permissionsList = permissions.ToList();

                ValidatorPermissionWithId(idList, permissionsList);

                foreach (var itemPermission in permissionsList)
                {
                    itemPermission.DataDeleted = DateTime.Now;
                }

                await Task.CompletedTask;
                return permissionsList;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if a permission with the specified ID or name exists in the provided IEnumerable of permissions. 
        /// Validates the IEnumerable of permissions and ensures that the ID and name are unique. 
        /// Returns true if either the ID or name exists, otherwise false.
        /// </summary>
        /// <param name="id">Boolean indicating if the ID is to be checked for existence.</param>
        /// <param name="name">Boolean indicating if the name is to be checked for existence.</param>
        /// <param name="permissions">IEnumerable of permissions to check against.</param>
        /// <param name="cancellationToken">Token for task cancellation.</param>
        /// <returns>True if the ID or name exists in the IEnumerable; otherwise, false.</returns>
        /// <exception cref="DomainException">Thrown if validation fails.</exception>
        public async Task<bool> ExistsAsync(bool id, bool name, IEnumerable<Permission> permissions, CancellationToken cancellationToken)
        {
            try
            {
                ValidatePermission(permissions);

                var idList = permissions.Select(r => r.Id);
                ValidateIds(idList);
                ValidatePermissionId(id, idList);

                var nameList = permissions.Select(r => r.Name);
                ValidatePermissionName(name, nameList);

                await Task.CompletedTask;
                return id || name;
            }
            catch (DomainException)
            {
                throw;
            }
        }

        /// <summary>
        /// Validates a IEnumerable of GUIDs to ensure it contains valid, non-empty identifiers. 
        /// Throws a DomainException if the IEnumerable is empty or any GUID is empty.
        /// </summary>
        /// <param name="ids">IEnumerable of GUIDs to validate.</param>
        /// <exception cref="DomainException">Thrown when the IEnumerable is empty or contains invalid GUIDs.</exception>
        private static void ValidateIds(IEnumerable<Guid> ids)
        {
            if (ids.Any(i => i.GetType() != typeof(Guid)) || ids.Any(i => i == Guid.Empty))
                throw DomainException.IdentityInvalidException.Identities(ids);
        }

        /// <summary>
        /// Validates the permission ID by checking if it already exists in the provided IEnumerable of IDs. 
        /// Throws a PermissionException if the ID already exists.
        /// </summary>
        /// <param name="id">Boolean indicating if the ID exists.</param>
        /// <param name="idList">IEnumerable of existing IDs to validate against.</param>
        /// <exception cref="PermissionException">Thrown when the permission ID already exists.</exception>
        private static void ValidatePermissionId(bool id, IEnumerable<Guid> idList)
        {
            if (id)
                throw PermissionException.PermissionAlreadyExistsException.FromExistingIds(idList);
        }

        /// <summary>
        /// Validates the uniqueness of a permission name. 
        /// If the name is already in use (indicated by the boolean parameter), a PermissionException is thrown with the existing names.
        /// </summary>
        /// <param name="name">Boolean indicating if the name already exists.</param>
        /// <param name="namesList">IEnumerable of existing permission names to be used in the exception message.</param>
        /// <exception cref="PermissionException">Thrown when the permission name already exists.</exception>
        private static void ValidatePermissionName(bool name, IEnumerable<string> namesList)
        {
            if (name)
                throw PermissionException.PermissionAlreadyExistsException.FromExistingNames(namesList);
        }

        /// <summary>
        /// Validates a IEnumerable of permissions to ensure it contains at least one permission. 
        /// Throws a PermissionException if the IEnumerable is empty or contains no permissions.
        /// </summary>
        /// <param name="permissions">IEnumerable of permissions to validate.</param>
        /// <exception cref="PermissionException">Thrown when the IEnumerable is empty or contains no permissions.</exception>
        private static void ValidatePermission(IEnumerable<Permission> permissions)
        {
            if (!permissions.Any())
                throw PermissionException.PermissionNotFoundException.NotFoundPermissions(permissions);
        }

        /// <summary>
        /// Validates that the IEnumerable of permissions contains at least one permission with an ID that matches one of the provided IDs. 
        /// Throws a PermissionException if no matching permissions are found or if the IEnumerable is empty.
        /// </summary>
        /// <param name="ids">Collection of GUIDs to match against the permissions.</param>
        /// <param name="permissions">IEnumerable of permissions to validate.</param>
        /// <exception cref="PermissionException">Thrown when no permissions with matching IDs are found or the IEnumerable is empty.</exception>
        private static void ValidatorPermissionWithId(IEnumerable<Guid> ids, IEnumerable<Permission> permissions)
        {
            if (!permissions.Any(p => ids.Contains(p.Id)))
                throw PermissionException.PermissionNotFoundException.NotFoundPermissions(permissions);
        }

        /// <summary>
        /// Validates that the IEnumerable of updated permissions contains at least one permission with an ID that matches one of the provided IDs. 
        /// Throws a PermissionException if no matching permissions are found or if the IEnumerable is empty.
        /// </summary>
        /// <param name="ids">Collection of GUIDs to match against the updated permissions.</param>
        /// <param name="permissionsUpdate">IEnumerable of updated permissions to validate.</param>
        /// <exception cref="PermissionException">Thrown when no permissions with matching IDs are found or the IEnumerable is empty.</exception>
        private static void ValidatePermissionsUpdate(IEnumerable<Guid> ids, IEnumerable<Permission> permissionsUpdate)
        {
            if (!permissionsUpdate.Any(p => ids.Contains(p.Id)))
                throw PermissionException.UpdatePermissionNotFoundException.CreateForPermissionsNotFound(permissionsUpdate);
        }

        /// <summary>
        /// Validates that the list of updated permissions is not empty and contains at least one permission. 
        /// Throws a PermissionException if the list is empty or contains no permissions.
        /// </summary>
        /// <param name="permissionsUpdate">List of updated permissions to validate.</param>
        /// <exception cref="PermissionException">Thrown when the list is empty or contains no updated permissions.</exception>
        private static void ValidatePermissionsApplyUpdate(List<Permission> permissionsUpdate)
        {
            if (permissionsUpdate.Count == 0)
                throw PermissionException.UpdatePermissionNotFoundException.CreateForNoUpdatesDetected(permissionsUpdate);
        }

        /// <summary>
        /// Applies updates to a list of original permissions based on a list of updated permissions. 
        /// It creates a new list of permissions where only those that have changed are updated, preserving creation data from the originals and setting update data.
        /// </summary>
        /// <param name="originalPermissions">List of original permissions to be updated.</param>
        /// <param name="updatedPermissions">List of updated permissions to apply.</param>
        /// <returns>A list of updated permissions with changes applied.</returns>
        private static List<Permission> ApplyUpdates(List<Permission> originalPermissions, List<Permission> updatedPermissions)
        {
            var originalPermissionDict = originalPermissions.ToDictionary(p => p.Id);
            var updatePermissionDict = updatedPermissions.ToDictionary(p => p.Id);

            return updatePermissionDict
                .Where(update => !ArePermissionsEqual(originalPermissionDict[update.Key], update.Value))
                .Select(update => {
                    var originalPermission = originalPermissionDict[update.Key];
                    var updatedPermission = update.Value;

                    updatedPermission.UserCreated = originalPermission.UserCreated;
                    updatedPermission.DataCreated = originalPermission.DataCreated;

                    originalPermission.UserUpdated = updatedPermission.UserUpdated;
                    originalPermission.DataUpdated = DateTime.Now;

                    return updatedPermission;
                }).ToList();
        }


        /// <summary>
        /// Compares two permissions to determine if they are equal based on their ID, name, description, and user creation data. 
        /// Returns false if either permission is null, if their roles differ, or if any of the specified properties do not match.
        /// </summary>
        /// <param name="originalPermission">The original permission to compare.</param>
        /// <param name="updatePermission">The updated permission to compare.</param>
        /// <returns>True if the permissions are considered equal; otherwise, false.</returns>
        private static bool ArePermissionsEqual(Permission? originalPermission, Permission? updatePermission)
        {
            if (originalPermission == null && updatePermission == null)
                return false;

            var rolesOriginalPermission = originalPermission.Roles.FirstOrDefault(o => o.Id == originalPermission.Id);
            var rolesUpdatePermission = updatePermission.Roles.FirstOrDefault(u => u.Id == updatePermission.Id);

            if (rolesOriginalPermission == null && rolesUpdatePermission != null)
                return false;


            return originalPermission.Id == updatePermission.Id &&
                   originalPermission.Name == updatePermission.Name &&
                   originalPermission.Description == updatePermission.Description &&
                   originalPermission.UserCreated == updatePermission.UserCreated;
        }

        /// <summary>
        /// Converts an IEnumerable to a List asynchronously. 
        /// Iterates through the enumerable and adds each item to a new list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The collection to convert.</param>
        /// <returns>A Task that represents the asynchronous operation, containing the converted list.</returns>
        private static async Task<List<T>> ConvertIEnumerableToList<T>(IEnumerable<T> enumerable)
        {
            var newList = new List<T>();
            foreach (var item in enumerable)
            {
                newList.Add(item);
            }

            return await Task.FromResult(newList);
        }
    }
}
