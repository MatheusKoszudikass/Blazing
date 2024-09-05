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
    public class UserEcommerceRepositoryFixtureTest : IClassFixture<RepositoryFixtureTest>
    {
        //User
        private readonly RepositoryFixtureTest _fixture;

        private readonly IEnumerable<UserDto> _user;
        private readonly IEnumerable<Guid> _userId;
        private readonly IEnumerable<UserDto> _UserToUpdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEcommerceRepositoryFixtureTest"/> class.
        /// </summary>
        /// <param name="fixture">The fixture used to set up the necessary data and dependencies.</param>
        public UserEcommerceRepositoryFixtureTest(RepositoryFixtureTest fixture)
        {
            _fixture = fixture;
            _user = _fixture.PeopleOfData.GetAddUsers();
            _userId = _fixture.PeopleOfData.GetUserId();
            _UserToUpdate = _fixture.PeopleOfData.GetUpdateUsers();
        }


        /// <summary>
        /// Tests the functionality of the UserEcommerceRepository class.
        /// </summary>
        [Fact]
        public async Task UserAllTest()
        {
            // Create a cancellation token with no cancellation.
            var cts = CancellationToken.None;

            // Test if users exist in the repository.
            var resultExistsFalse = await _fixture.UserEcommerceRepository.ExistsAsync(_user, cts);

            // Add users to the repository.
            var resultAdd = await _fixture.UserEcommerceRepository.AddUsers(_user, cts);

            // Get all users from the repository.
            var resultUserAll = await _fixture.UserEcommerceRepository.GetAllUsers(cts);

            // Update users in the repository.
            var resultToUpdate = await _fixture.UserEcommerceRepository.UpdateUsers(_userId, _UserToUpdate, cts);

            // Get users by ID from the repository.
            var resultById = await _fixture.UserEcommerceRepository.GetUsersById(_userId, cts);

            // Delete users from the repository.
            var resultDeleteUser = await _fixture.UserEcommerceRepository.DeleteUsers(_userId, cts);

            // Assert that the results are not null.
            Assert.False(resultExistsFalse);
            Assert.NotNull(resultAdd);
            Assert.NotNull(resultUserAll);
            Assert.NotNull(resultById);
            Assert.NotNull(resultToUpdate);
            Assert.NotNull(resultDeleteUser);

            // Assert that the results of the repository methods match the expected results.
            await TaskCompletedTaskUserDto(_user, resultAdd);
            await TaskCompletedTaskUserDto(_UserToUpdate, resultToUpdate);
            await TaskCompletedTaskUserDto(resultToUpdate, resultById);
            await TaskCompletedTaskUserDto(resultById, resultDeleteUser);
        }

        /// <summary>
        /// Compares two collections of UserDto objects and asserts that they are equal.
        /// </summary>
        /// <param name="originalUserDto">The original collection of UserDto objects.</param>
        /// <param name="checkUserDto">The collection of UserDto objects to check against the original.</param>
        /// <returns>A Task that completes when the comparison is complete.</returns>
        private static Task TaskCompletedTaskUserDto(IEnumerable<UserDto?> originalUserDto, IEnumerable<UserDto?> checkUserDto)
        {
            foreach (var itemAdded in checkUserDto)
            {
                var userAdd = originalUserDto.FirstOrDefault(u => u.Id == itemAdded.Id);

                Assert.Equal(itemAdded.Id, userAdd.Id);
                Assert.Equal(itemAdded.Status, userAdd.Status);
                Assert.Equal(itemAdded.FirstName, userAdd.FirstName);
                Assert.Equal(itemAdded.LastName, userAdd.LastName);
                Assert.Equal(itemAdded.UserName, userAdd.UserName);
                Assert.Equal(itemAdded.Email, userAdd.Email);
                Assert.Equal(itemAdded.PasswordHash, userAdd.PasswordHash);
                Assert.Equal(itemAdded.PhoneNumber, userAdd.PhoneNumber);
            }

            return Task.CompletedTask;
        }

        private static Task TaskCompletedTaskAddressUser(IEnumerable<AddressDto?> originalAddressDto, IEnumerable<AddressDto?> checkAddressDto)
        {
            foreach (var itemAdded in checkAddressDto)
            {
                var addressAdd = originalAddressDto.FirstOrDefault(u => u.Id == itemAdded.Id);

                Assert.Equal(itemAdded.UserId, addressAdd.UserId);
                Assert.Equal(itemAdded.Street, addressAdd.Street);
                Assert.Equal(itemAdded.City, addressAdd.City);
                Assert.Equal(itemAdded.State, addressAdd.State);
                Assert.Equal(itemAdded.PostalCode, addressAdd.PostalCode);
            }

            return Task.CompletedTask;
        }
    }
}
