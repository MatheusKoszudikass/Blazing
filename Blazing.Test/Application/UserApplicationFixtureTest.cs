using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;

namespace Blazing.Test.Application
{
    /// <summary>
    /// This class represents a test fixture for the UserApplication. It tests the functionality of the UserApplication class.
    /// </summary>
    public class UserApplicationFixtureTest(ApplicationFixtureTest fixture) : IClassFixture<ApplicationFixtureTest>
    {
        private readonly IEnumerable<UserDto> _users = fixture.PeopleOfData.GetAddUsers();
        private readonly IEnumerable<Guid> _idUsers = fixture.PeopleOfData.GetUserId();
        private readonly IEnumerable<UserDto> _usersUpdate = fixture.PeopleOfData.GetUpdateUsers();

        /// <summary>
        /// Tests the functionality of the UserApplication class.
        /// </summary>
        /// <returns>A task that completes when the test is complete.</returns>
        [Fact]
        public async Task UserApplicationTestAll()
        {
            var cts = CancellationToken.None;

            var originalUsers = _users.ToList();
            var updatedUsers = _usersUpdate.ToList();

            var resultBool = await fixture.UserAppService.ExistsUsers(false, false, false,_users, cts);

            var resultAddAsync = await fixture.UserAppService.AddUsers(originalUsers, cts);

            var resultUpdateAsync = await fixture.UserAppService.UpdateUsers(_idUsers, originalUsers, updatedUsers, cts);

            var resultByIdAsync = await fixture.UserAppService.GetUserById(_idUsers, updatedUsers, cts);

            var resultGetAllAsync = await fixture.UserAppService.GetAllUsers(updatedUsers, cts);

            var resultDeletedAsync = await fixture.UserAppService.DeleteUsers(_idUsers, updatedUsers, cts);

            Assert.False(resultBool);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultUpdateAsync);
            Assert.NotNull(resultByIdAsync);
            Assert.NotNull(resultGetAllAsync);
            Assert.NotNull(resultDeletedAsync);

            CompareUsers(originalUsers, resultAddAsync);
            CompareUsers(updatedUsers, resultUpdateAsync);
            CompareUsers(updatedUsers, resultByIdAsync);
            CompareUsers(updatedUsers, resultGetAllAsync);
            CompareUsers(updatedUsers, resultDeletedAsync);

        }

        /// <summary>
        /// Compares the properties of two UserDto objects.
        /// </summary>
        /// <param name="originalUsers">The original UserDto objects.</param>
        /// <param name="updatedUsers">The updated UserDto objects.</param>
        private static void CompareUsers(IEnumerable<UserDto> originalUsers, IEnumerable<UserDto?> updatedUsers)
        {
            var userDto = originalUsers.ToList();
            foreach (var item in updatedUsers)
            {
                var user = userDto.FirstOrDefault(x => x.Id == item.Id);
                Assert.NotNull(user);
                Assert.Equal(item.Id, user.Id);
                Assert.Equal(item.UserName, user.UserName);
                Assert.Equal(item.Email, user.Email);
                Assert.Equal(item.FirstName, user.FirstName);
                Assert.Equal(item.LastName, user.LastName);
                Assert.Equal(item.Status, user.Status);
                Assert.Equal(item.PasswordHash, user.PasswordHash);
                Assert.Equal(item.PhoneNumber, user.PhoneNumber);
            }
        }
    }
}
