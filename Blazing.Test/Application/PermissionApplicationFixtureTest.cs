using Blazing.Application.Dto;
using Blazing.Domain.Entities;
using Blazing.Domain.Exceptions.Permission;
// ReSharper disable All

namespace Blazing.Test.Application
{
    public class PermissionApplicationFixtureTest(ApplicationFixtureTest fixture) : IClassFixture<ApplicationFixtureTest>
    {
        private readonly List<PermissionDto> _permission = fixture.PeopleOfData.GetPermissions();
        private readonly List<PermissionDto> _permissionUpdate = fixture.PeopleOfData.UpdatePermissions();
        private readonly IEnumerable<Guid> _idPermission = fixture.PeopleOfData.GetPermissionId();

        /// <summary>
        /// Fixture test class for permission-related application services.
        /// This class sets up test data for permissions and uses the fixture for shared configuration.
        /// </summary>
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
        /// Adds a list of permissions using the application service and verifies the result.
        /// Ensures that the added permissions match the input data.
        /// </summary>
        /// <param name="permission">List of permissions to be added.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task AddPermission(List<PermissionDto> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.AddPermission(permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<PermissionDto>>(result);
            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Updates a list of permissions based on their IDs using the application service and verifies the result.
        /// Ensures that the updated permissions match the input data for updates.
        /// </summary>
        /// <param name="id">Collection of permission IDs to be updated.</param>
        /// <param name="permission">Original list of permissions.</param>
        /// <param name="updatePermission">List of updated permissions to apply.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task UpdatedPermission(IEnumerable<Guid> id, List<PermissionDto> permission,
            List<PermissionDto> updatePermission, CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.UpdatePermission(
                id, permission, updatePermission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<PermissionDto?>>(result);
            ComparePermission(updatePermission, result.ToList());
        }

        /// <summary>
        /// Retrieves all permissions using the application service and verifies the result.
        /// Ensures that the retrieved permissions match the input data.
        /// </summary>
        /// <param name="permission">The original list of permissions.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task GetAll(IEnumerable<PermissionDto> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.GetAll(permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<PermissionDto>>(result);
            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Retrieves permissions by their IDs using the application service and verifies the result.
        /// Ensures that the retrieved permissions match the original input data.
        /// </summary>
        /// <param name="id">The IDs of the permissions to retrieve.</param>
        /// <param name="permission">The original list of permissions for comparison.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task GetById(IEnumerable<Guid> id, IEnumerable<PermissionDto> permission, CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.GetById(_idPermission, permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<PermissionDto?>>(result);
            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Deletes permissions by their IDs using the application service and verifies the result.
        /// Ensures that the deleted permissions match the original input data.
        /// </summary>
        /// <param name="id">The IDs of the permissions to delete.</param>
        /// <param name="permission">The original list of permissions for comparison.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task DeletePermission(IEnumerable<Guid> id, IEnumerable<PermissionDto> permission,
            CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.DeletePermission(
                id, permission, cts);

            Assert.NotNull(result);
            Assert.IsType<List<PermissionDto>>(result);
            ComparePermission(permission, result.ToList());
        }

        /// <summary>
        /// Checks if any permissions exist based on the provided criteria using the application service.
        /// Verifies that the result indicates no existing permissions.
        /// </summary>
        /// <param name="permission">The list of permissions to check for existence.</param>
        /// <param name="cts">Cancellation token for the async operation.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        private async Task ExistsPermission(IEnumerable<PermissionDto> permission, CancellationToken cts)
        {
            var result = await fixture.PermissionAppService.ExistsPermission(
                false, false, permission, cts);

            Assert.False(result);
            Assert.IsType<bool>(result);
        }

        /// <summary>
        /// Compares original permission objects with updated permission objects to ensure they match.
        /// Asserts that each property of the original permission matches the corresponding property of the updated permission.
        /// </summary>
        /// <param name="permission">The collection of original permissions.</param>
        /// <param name="permissionUpdate">The list of updated permissions.</param>
        private static void ComparePermission(IEnumerable<PermissionDto> permission,
            List<PermissionDto?> permissionUpdate)
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
