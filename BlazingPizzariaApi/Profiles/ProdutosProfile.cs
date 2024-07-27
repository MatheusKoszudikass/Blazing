using AutoMapper;
using BlazingPizza.Api.Entites;
using BlazingPizza.Models.DTOs;
using BlazingPizzaria.Models.DTOs;

namespace BlazingPizza.Api.Profiles
{
    public class ProdutosProfile : Profile
    {
        public ProdutosProfile()
        {
                CreateMap<Atributo, AtributosDto>().ReverseMap();
                CreateMap<AtualizarQuantidadeCarrinho, CarrinhoDeItemAtualizarQuantidadeDto>();
                CreateMap<Avaliacao, AvaliacaoDto>().ReverseMap();
                CreateMap<CarrinhoDeCompra, CarrinhoDeCompraDto>().ReverseMap();
                CreateMap<CarrinhoDeItem, CarrinhoDeItemsDto>().ReverseMap();
                CreateMap<CarrinhoItemAdd, CarrinhoItemAddDto>().ReverseMap();
                CreateMap<Categoria, CategoriasDto>().ReverseMap();
                CreateMap<Dimensoes, DimensoesDto>().ReverseMap();
                CreateMap<Disponibilidade, DisponibilidadeDto>().ReverseMap();
                CreateMap<Imagem, ImagemDto>().ReverseMap();
                CreateMap<Produto, ProdutoDto>().ReverseMap();
                CreateMap<Revisao, RevisaoDto>().ReverseMap();
                CreateMap<Usuario, UsuariosDto>().ReverseMap();
        }
    }
}
