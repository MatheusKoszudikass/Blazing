using AutoMapper;
using Blazing.Application.Mappings;
using Blazing.Domain.Entities;
using Blazing.Domain.Interfaces.Repository;
using Blazing.infrastructure.Data.Repositories;
using Blazing.infrastructure.Dependency;
using Blazing.Test.Data;
using BlazingPizzaTest.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Test.Infrastructure
{
    public class ProductRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly PeopleOfData _peopleOfData = new();
         
        public ProductRepositoryTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlazingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task ProductRepository()
        {
            var categoryId = _peopleOfData.CategoryId1;

            var productsId = _peopleOfData.ProductId;

            var productsId1 = _peopleOfData.ProductId1;

            var products = _peopleOfData.GetProducts();

            var productsFilterCategory = products.Where(x => x.CategoryId == categoryId).ToList();

            var updateProduct = _peopleOfData.UpdateProduct();



            var ids = _peopleOfData.GetIdsProduct();


            await using var DbContext = new MockDb().CreateDbContext();

            var injectServiceApi = new DependencyInjection(DbContext, _mapper);

            var productRepository = new ProductInfrastructureRepository(injectServiceApi);


            var resultAddAsync = await productRepository.AddAsync(products);

            var resultUpdateAsync = await productRepository.UpdateAsync(productsId,updateProduct);

            var resultGetByCategoryIdAsync = await productRepository.GetByCategoryIdAsync(categoryId);

            var resultDeleteAsync = await productRepository.DeleteByIdAsync(ids);

            var resultGetByIdAsync = await productRepository.GetByIdAsync(productsId1);

            var resultGetAllAsync = await productRepository.GetAllAsync();


            Assert.True(resultAddAsync.Any());
            Assert.Equal(products, resultAddAsync);


            Assert.NotNull(resultUpdateAsync);
            Assert.Equal(updateProduct, resultUpdateAsync);


            Assert.True(resultGetByCategoryIdAsync.Any());
            Assert.Equal(productsFilterCategory, resultGetByCategoryIdAsync);


            Assert.True(resultDeleteAsync.Any());


            Assert.NotNull(resultGetByIdAsync);


            Assert.True(resultGetAllAsync.Any());
            Assert.Equal(resultGetAllAsync, resultGetByCategoryIdAsync);
        }
    }
}
