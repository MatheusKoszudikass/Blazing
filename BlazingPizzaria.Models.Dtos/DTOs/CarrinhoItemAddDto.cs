using BlazingPizzaria.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Models.DTOs
{
    public class CarrinhoItemAddDto
    {
        public Guid CarrinhoId { get; set; }

        public Guid ProdutoId { get; set; }

        public ProdutoDto? Produtos { get; set; }

        public int Quantidade { get; set; }
    }
}
