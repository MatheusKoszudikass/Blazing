using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Identity.Entities;

namespace Blazing.Test.Infrastructure
{
    /// <summary>
    /// Test class for the UserDtoRepository.
    /// </summary>
    /// <remarks>
    /// This class is used to test the UserDtoRepository methods.
    /// It uses the UserDtoRepositoryFixture to set up the necessary data and dependencies.
    /// </remarks>
    public class UserEcommerceRepositoryFixtureTest(RepositoryFixtureTest fixture) : IClassFixture<RepositoryFixtureTest>
    {
        private readonly IEnumerable<UserDto> _user = fixture.PeopleOfData.GetAddUsers();
        private readonly IEnumerable<Guid> _userId = fixture.PeopleOfData.GetUserId();
        private readonly IEnumerable<UserDto> _UserToUpdate = fixture.PeopleOfData.GetUpdateUsers();


        /// <summary>
        /// Tests the functionality of the UserEcommerceRepository class.
        /// </summary>
        [Fact]
        public async Task UserAllTest()
        {
            const int page = 1;
            const int pageSize = 3;
            // Create a cancellation token with no cancellation.
            var cts = CancellationToken.None;

            var originalUser = _user.ToList();
            var updatedUser = _UserToUpdate.ToList();

            // Test if users exist in the repository.
            var resultExistsFalse = await fixture.UserEcommerceRepository.ExistsAsync(originalUser, cts);

            // Add users to the repository.
            var resultAdd = await fixture.UserEcommerceRepository.AddUsers(originalUser, cts);

            // Update users in the repository.
            var resultToUpdate = await fixture.UserEcommerceRepository.UpdateUsers(_userId, updatedUser, cts);

            // Get all users from the repository.
            var resultUserAll = await fixture.UserEcommerceRepository.GetAllUsers(page, pageSize, cts);

            // Get users by ID from the repository.
            var resultById = await fixture.UserEcommerceRepository.GetUsersById(_userId, cts);

            // Delete users from the repository.
            var resultDeleteUser = await fixture.UserEcommerceRepository.DeleteUsers(_userId, cts);

            // Assert that the results are not null.
            Assert.False(resultExistsFalse);
            Assert.NotNull(resultAdd);
            Assert.NotNull(resultUserAll);
            Assert.NotNull(resultById);
            Assert.NotNull(resultToUpdate);
            Assert.NotNull(resultDeleteUser);

            Assert.IsType<List<UserDto>>(resultAdd);
            Assert.IsType<List<UserDto>>(resultUserAll);
            Assert.IsType<List<UserDto>>(resultById);
            Assert.IsType<List<UserDto>>(resultToUpdate);
            Assert.IsType<List<UserDto>>(resultDeleteUser);

            CompareUsers(originalUser, resultAdd);
            CompareUsers(updatedUser, resultToUpdate);
            CompareUsers(updatedUser, resultById);
            CompareUsers(updatedUser, resultUserAll);
            CompareUsers(updatedUser, resultDeleteUser);
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
