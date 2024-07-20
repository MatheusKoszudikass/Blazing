using BlazingPizzaria.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Models.DTOs
{
    public class CarrinhoItemAddDtos
    {
        public int CarrinhoId { get; set; }

        public int ProdutoId { get; set; }

        public ProdutoDtos? Produtos { get; set; }

        public int Quantidade { get; set; }
    }
}
