using Blazing.Api.Controllers.Product;
using Blazing.Ecommerce.Repository;
using Blazing.Test.Data;
using BlazingPizzaTest.Controller;
using BlazingPizzaTest.Data;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Test.Api.Controller
{
    public class ControllerRepositoryFixtureTest : IDisposable
    {
        /// <summary>
        /// Gets the peopleOfData instance.
        /// </summary>
        public PeopleOfData PeopleOfData { get; }


        public ProductController ProductController { get; }



        public ControllerRepositoryFixtureTest()
        {
            PeopleOfData = new PeopleOfData();

            var loggerProduct = new Mock<ILogger<ProductController>>();
            var productMockInfra = new Mock<IProductInfrastructureRepository>();

             ProductController = new ProductController(loggerProduct.Object, productMockInfra.Object);
        }

        public void Dispose()
        {
            
        }
    }
}
