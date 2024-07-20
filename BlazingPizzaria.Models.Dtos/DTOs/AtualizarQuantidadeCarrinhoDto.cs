using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Models.DTOs
{
    public class CarrinhoDeItemAtualizarQuantidadeDto
    {
        public int CarrinhoDeItemId { get; set; }

        public int Quantidade { get; set; }
    }
}
