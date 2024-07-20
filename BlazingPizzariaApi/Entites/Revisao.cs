using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Api.Entites
{
    public class Revisao
    {
        [Key]
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Comentario { get; set; }
        public DateTime Data { get; set; }

    }
}
