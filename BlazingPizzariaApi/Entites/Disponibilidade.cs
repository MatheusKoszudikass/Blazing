using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    public class Disponibilidade
    {
        [Key]
        public int Id { get; set; }
        public bool EstaDisponivel { get; set; }
        public DateTime DataEstimadaDeEntrega { get; set; }
    }
}
