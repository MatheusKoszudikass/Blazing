using AutoMapper;
using BlazingPizza.Api.Dependencias;
using BlazingPizza.Api.Profiles;
using BlazingPizza.Api.Repositories.Services;
using BlazingPizzaria.Models.DTOs;
using BlazingPizzaTest.Data;
using BlazingPizzaTest.Helps;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Moq;
using static BlazingPizzaTest.Depedencia.PriorityOrderer;


namespace BlazingPizzaTest.TestUnitarios
{
    [TestCaseOrderer("Namespace.PriorityOrderer", "AssemblyName")]
    public class CategoriaTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<CategoriaServices>> _loggerMock;

        public CategoriaTest()
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProdutosProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<CategoriaServices>>();
        }

        [Fact, TestPriority(2)]
        public async Task Categoria()
        {
            //Classe de povoamento  de dados 
            PeopleOfData peopleOfData = new PeopleOfData();

            var categorias = peopleOfData.AddCategoria();

            var getCategoria = peopleOfData.GetItemCategoria();

            var updateCategoria = peopleOfData.UpdateCategoria();

            var ids = peopleOfData.GetIdsCategoria();



            await using var DbContext = new MockDb().CreateDbContext();

            var injectServiceApi = new InjectServicesApi(DbContext, _mapper);

            var categoriaService = new CategoriaServices(injectServiceApi);




            var resultAddCategoria = await categoriaService.AddCategoria(categorias);

            var resultUpdateCategoria = await categoriaService.UpdateCategoria(peopleOfData.CategoriaId, updateCategoria);

            var resultGetItemCategoria = await categoriaService.GetItemCategoria(peopleOfData.CategoriaId);

            var resultGetCategoria = await categoriaService.GetIAllCategoria();

            var resutDeleteCategoria = await categoriaService.DeleteCategoria(ids);




            Assert.True(resultAddCategoria.Any());
            Assert.NotNull(resultGetItemCategoria);

        }
    }
}
