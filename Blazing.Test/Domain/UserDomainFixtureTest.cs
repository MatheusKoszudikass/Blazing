using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Test.Domain
{
    public class UserDomainFixtureTest(DomainFixtureTest domainFixtureTest) : IClassFixture<DomainFixtureTest>
    {
        private readonly IEnumerable<UserDto> _users = domainFixtureTest.PeopleOfData.GetAddUsers();
        private readonly IEnumerable<Guid> _idUsers = domainFixtureTest.PeopleOfData.GetUserId();
        private readonly IEnumerable<UserDto> _usersToUpdate = domainFixtureTest.PeopleOfData.GetUpdateUsers();

        [Fact]
        public async Task UserDomainTestAll()
        {
            var cts = CancellationToken.None;

            var users = domainFixtureTest.Mapper.Map<IEnumerable<User>>(_users);
            var originalUsers = users.ToList();
            var userToUpdate = domainFixtureTest.Mapper.Map<IEnumerable<User>>(_usersToUpdate);
            var userToUpdated = userToUpdate.ToList();

            var resultBool = await domainFixtureTest.UserDomainService.UserExistsAsync(false, false, false, originalUsers, cts);

            var resultAddAsync = await domainFixtureTest.UserDomainService.Add(originalUsers, cts);

            var resultToUpdate = await domainFixtureTest.UserDomainService.Update(_idUsers, originalUsers,userToUpdated, cts);

            var resultGetById = await domainFixtureTest.UserDomainService.GetById(_idUsers, userToUpdated, cts);

            var resultGetAll = await domainFixtureTest.UserDomainService.GetAll(userToUpdated, cts);

            var resultDeleted = await domainFixtureTest.UserDomainService.Delete(_idUsers, userToUpdated, cts);

            Assert.False(resultBool);
            Assert.NotNull(resultAddAsync);
            Assert.NotNull(resultToUpdate);
            Assert.NotNull(resultGetById);
            Assert.NotNull(resultGetAll);
            Assert.NotNull(resultDeleted);


            CompareUser(originalUsers, resultAddAsync);
            CompareUser(userToUpdated, resultToUpdate);
            CompareUser(userToUpdated, resultGetById);
            CompareUser(userToUpdated, resultGetAll);
            CompareUser(userToUpdated, resultDeleted);

        }

        private static void CompareUser(IEnumerable<User> originalUsers, IEnumerable<User?> updatedUsers)
        {
            var enumerable = originalUsers.ToList();
            foreach (var updatedUser in updatedUsers)
            {
                var userAdd = enumerable.FirstOrDefault(u => u.Id == updatedUser.Id);
                Assert.Equal(updatedUser.Status, userAdd.Status);
                Assert.Equal(updatedUser.FirstName, userAdd.FirstName);
                Assert.Equal(updatedUser.LastName, userAdd.LastName);
                Assert.Equal(updatedUser.UserName, userAdd.UserName);
                Assert.Equal(updatedUser.Email, userAdd.Email);
                Assert.Equal(updatedUser.PasswordHash, userAdd.PasswordHash);
                Assert.Equal(updatedUser.PhoneNumber, userAdd.PhoneNumber);
                Assert.Equal(updatedUser.DataCreated, userAdd.DataCreated);
                Assert.Equal(updatedUser.DataUpdated, userAdd.DataUpdated);
                Assert.Equal(updatedUser.DataDeleted, userAdd.DataDeleted);
            }
        }
    }
}
