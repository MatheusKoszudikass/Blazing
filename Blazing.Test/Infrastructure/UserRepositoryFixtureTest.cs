using Blazing.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Test method for the UserDtoRepository methods.
        /// </summary>
        /// <remarks>
        /// This method tests the AddProducts, UpdateProduct, GetProductsByCategoryId, GetProductById, GetAll and DeleteProducts methods of the ProductRepository.
        /// </remarks>
        [Fact]
        public async Task UserAllTest()
        {
            var cts = CancellationToken.None;

            var resultExistsFalse = await _fixture.UserEcommerceRepository.ExistsAsync(_user, cts);

            var resultAdd = await _fixture.UserEcommerceRepository.AddUsers(_user, cts);

            var resultUserAll = await _fixture.UserEcommerceRepository.GetAllUsers(cts);

            var resultToUpdate = await _fixture.UserEcommerceRepository.UpdateUsers(_userId, _UserToUpdate, cts);

            var resultById = await _fixture.UserEcommerceRepository.GetUsersById(_userId, cts);

            Assert.False(resultExistsFalse);
            Assert.NotNull(resultAdd);
            Assert.NotNull(resultUserAll);
            Assert.NotNull(resultById);
            Assert.NotNull(resultToUpdate);
        }
        
    }
}
