using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsável pela revião do produto feita pelo usuário.
    /// </summary>
    public class Revisao
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string? Usuario { get; set; }
        public string? Comentario { get; set; }
        public DateTime Data { get; set; }

    }
}
