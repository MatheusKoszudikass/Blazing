using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável por dar informações se o produto está dispinivel.
    /// </summary>
    public class Disponibilidade
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public bool EstaDisponivel { get; set; }
        public DateTime DataEstimadaDeEntrega { get; set; }
    }
}
