using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Api.Entites
{
    /// <summary>
    /// Entidade responsavel pelo atributos do produto
    /// </summary>
    public class Atributo
    {
        //Criação do id com a inicialização do Guid.NewGuid.
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string? Cor { get; set; }
        public string? Material { get; set; }
        public string? Modelo { get; set; }
    }
}
