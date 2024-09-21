using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions;
using Blazing.Domain.Exceptions.Permission;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
// ReSharper disable All

namespace Blazing.Test.Domain
{
    /// <summary>
    /// Test fixture for the Permission domain, utilizing a shared setup from DomainFixtureTest.
    /// Initializes collections of permissions and their updates for use in unit tests.
    /// </summary>
    /// <param name="fixture">An instance of DomainFixtureTest providing shared data and services.</param>
    public class PermissionDomainFixtureTest(DomainFixtureTest fixture) : IClassFixture<DomainFixtureTest>
    {
        private readonly List<Permission> _permission =  fixture.Mapper.Map<List<Permission>>(fixture.PeopleOfData.GetPermissions());
        private readonly List<Permission> _permissionUpdate = fixture.Mapper.Map<List<Permission>>(fixture.PeopleOfData.UpdatePermissions());
        private readonly IEnumerable<Guid> _idPermission = fixture.PeopleOfData.GetPermissionId();

        /// <summary>
        /// Tests the complete functionality of the permission domain by performing a series of operations:
        /// adding a permission, updating a permission, retrieving all permissions, getting a permission by ID,
        /// deleting a permission, and checking for the existence of a permission. 
        /// This method uses a cancellation token to manage asynchronous operations.
        /// </summary>
        /// <returns>A Task representing the asynchronous test operation.</returns>
        [Fact]
        public async Task PermissionDomainTestAll()
        {
            var cts = CancellationToken.None;
            await AddPermission(_permission, cts);
            await UpdatedPermission(_idPermission, _permission, _permissionUpdate, cts);
            await GetAll(_permissionUpdate, cts);
            await GetById(_idPermission, _permissionUpdate, cts);
            await DeletePermission(_idPermission, _permissionUpdate, cts);
            await ExistsPermission(_permissionUpdate, cts);
        }

        /// <summary>
        /// Adds a list of permissions using the permission domain service and validates the result.
        /// Asserts that the result is not null, is of the expected type, and compares the added permissions 
        /// with the expected values to ensure correctness.
        /// </summary>
        /// <param name="permission">List of permissions to add.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task AddPermission(List<Permission> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.Add(permission, cts);

            await AddPermissionException(permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<Permission>>(result);

            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Tests that adding an empty list of permissions throws a PermissionNotFoundException. 
        /// Asserts that the expected exception is thrown when attempting to add an invalid permission list.
        /// </summary>
        /// <param name="permission">List of permissions used for context but not directly in this test.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task AddPermissionException(List<Permission> permission, CancellationToken cts)
        {
            await Assert.ThrowsAsync<PermissionException.PermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.Add(new List<Permission>(), cts));
        }

        /// <summary>
        /// Updates a list of permissions based on the provided IDs and validates the result.
        /// Asserts that the result is not null, is of the expected type, and compares the updated permissions 
        /// with the expected values to ensure correctness.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to update.</param>
        /// <param name="permission">List of current permissions before the update.</param>
        /// <param name="updatePermission">List of permissions containing the updated values.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task UpdatedPermission(IEnumerable<Guid> id, List<Permission> permission,
            List<Permission> updatePermission, CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.Update
                (id, permission, updatePermission, cts);

           await UpdatedPermissionException(id, permission, updatePermission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<Permission?>>(result);
            ComparePermission(updatePermission, result.ToList());
        }

        /// <summary>
        /// Tests that updating permissions throws the expected exceptions for various invalid scenarios. 
        /// Asserts that a DomainException is thrown for invalid IDs, a PermissionNotFoundException for non-existent permissions, 
        /// and an UpdatePermissionNotFoundException when attempting to update with invalid data.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to update.</param>
        /// <param name="permission">List of current permissions used for validation.</param>
        /// <param name="updatePermission">List of permissions intended for the update.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task UpdatedPermissionException(IEnumerable<Guid> id, List<Permission> permission, 
            List<Permission> updatePermission, CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () =>
                await fixture.PermissionDomainService.Update(new [] { Guid.Empty }, permission, updatePermission, cts));

            await Assert.ThrowsAsync<PermissionException.PermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.Update(id: new [] { Guid.NewGuid() }, permission, updatePermission, cts));

            await Assert.ThrowsAsync<PermissionException.UpdatePermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.Update(id, permission, permission, cts));
        }

        /// <summary>
        /// Retrieves all permissions using the permission domain service and validates the result.
        /// Asserts that the result is not null, is of the expected type, and compares the retrieved permissions 
        /// with the expected values to ensure correctness.
        /// </summary>
        /// <param name="permission">List of permissions to validate against the retrieved results.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task GetAll(IEnumerable<Permission> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.GetAll(permission, cts);

            await GetAllException(permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<Permission>>(result);

            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Tests that retrieving permissions throws a PermissionNotFoundException when an empty list is provided.
        /// Asserts that the expected exception is thrown when attempting to get permissions with invalid input.
        /// </summary>
        /// <param name="permission">List of permissions used for context but not directly in this test.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task GetAllException(IEnumerable<Permission> permission, CancellationToken cts)
        {
            await Assert.ThrowsAsync<PermissionException.PermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.GetAll(new List<Permission>(), cts));
        }

        /// <summary>
        /// Retrieves permissions by their IDs using the permission domain service and validates the result.
        /// Asserts that the result is not null, is of the expected type, and compares the retrieved permissions 
        /// with the expected values to ensure correctness.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to retrieve.</param>
        /// <param name="permission">List of permissions to validate against the retrieved results.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task GetById(IEnumerable<Guid> id, IEnumerable<Permission> permission, CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.GetById(_idPermission, permission, cts);
                
            await GetByIdException(permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<Permission?>>(result);
            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Tests that retrieving permissions by ID throws the expected exceptions for various invalid scenarios. 
        /// Asserts that a DomainException is thrown for invalid IDs and a PermissionNotFoundException for non-existent permissions.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to validate against.</param>
        /// <param name="permission">List of permissions used for context but not directly in this test.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task GetByIdException(IEnumerable<Permission> permission,
            CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () =>
                await fixture.PermissionDomainService.GetById(id: new List<Guid> { Guid.Empty }, permission, cts));

            await Assert.ThrowsAsync<PermissionException.PermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.GetById(id: new List<Guid> { Guid.NewGuid() }, permission, cts));
        }

        /// <summary>
        /// Deletes permissions based on their IDs using the permission domain service and validates the result.
        /// Asserts that the result is not null, is of the expected type, and compares the deleted permissions 
        /// with the expected values to ensure correctness.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to delete.</param>
        /// <param name="permission">List of permissions to validate against the deleted results.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task DeletePermission(IEnumerable<Guid> id, IEnumerable<Permission> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.Delete(
                id, permission, cts);

            await DeletePermissionExcepition(id, permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<Permission>>(result);
            ComparePermission(permission, result);
        }

        /// <summary>
        /// Tests that deleting permissions throws the expected exceptions for various invalid scenarios.
        /// Asserts that a DomainException is thrown for invalid IDs and a PermissionNotFoundException for non-existent permissions.
        /// </summary>
        /// <param name="id">Collection of GUIDs representing the IDs of permissions to validate against.</param>
        /// <param name="permission">List of permissions used for context but not directly in this test.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task DeletePermissionExcepition(IEnumerable<Guid> id, IEnumerable<Permission> permission,
            CancellationToken cts)
        {
            await Assert.ThrowsAsync<DomainException.IdentityInvalidException>(async () =>
                await fixture.PermissionDomainService.GetById(id: new List<Guid> { Guid.Empty }, permission, cts));

            await Assert.ThrowsAsync<PermissionException.PermissionNotFoundException>(async () =>
                await fixture.PermissionDomainService.GetById(id: new List<Guid> { Guid.NewGuid() }, permission, cts));
        }

        /// <summary>
        /// Checks if any permissions exist based on the provided list and validates the result.
        /// Asserts that the result is false, indicating that no permissions were found, and confirms the result type is boolean.
        /// </summary>
        /// <param name="permission">Collection of permissions to check for existence.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task ExistsPermission(IEnumerable<Permission> permission, CancellationToken cts)
        {
            var result = await fixture.PermissionDomainService.ExistsAsync(
                false, false,permission, cts);

            await ExistsPermissionException(permission, cts);

            Assert.False(result);
            Assert.IsType<bool>(result);
        }

        /// <summary>
        /// Tests that the existence check for permissions throws the expected exceptions for various scenarios.
        /// Asserts that a PermissionAlreadyExistsException is thrown when checking for existing permissions with specific flags set.
        /// </summary>
        /// <param name="permission">Collection of permissions used to trigger the exception checks.</param>
        /// <param name="cts">Cancellation token for managing the asynchronous operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task ExistsPermissionException(IEnumerable<Permission> permission, CancellationToken cts)
        {
            await Assert.ThrowsAsync<PermissionException.PermissionAlreadyExistsException>(async () =>
                await fixture.PermissionDomainService.ExistsAsync(true, false, permission, cts));

            await Assert.ThrowsAsync<PermissionException.PermissionAlreadyExistsException>(async () =>
                await fixture.PermissionDomainService.ExistsAsync(false, true, permission, cts));
        }

        /// <summary>
        /// Compares two collections of permissions to ensure they have the same properties.
        /// Asserts that each property (ID, Name, Description, UserCreated, UserUpdated) of the original permissions matches the updated permissions.
        /// </summary>
        /// <param name="permission">The original collection of permissions to compare.</param>
        /// <param name="permissionUpdate">The updated collection of permissions to compare against the original.</param>
        private static void ComparePermission(IEnumerable<Permission> permission,
            IEnumerable<Permission> permissionUpdate)
        {
            foreach (var itemOriginalPermission in permission)
            {
                var itemUpdatePermission = permissionUpdate?.Where(p => p.Id == itemOriginalPermission.Id).FirstOrDefault();
                Assert.Equal(itemOriginalPermission.Id, itemUpdatePermission.Id);
                Assert.Equal(itemOriginalPermission.Name, itemUpdatePermission.Name);
                Assert.Equal(itemOriginalPermission.Description, itemUpdatePermission.Description);
                Assert.Equal(itemOriginalPermission.UserCreated, itemUpdatePermission.UserCreated);
                Assert.Equal(itemOriginalPermission.UserUpdated, itemUpdatePermission.UserUpdated);
            }
        }
    }
}
