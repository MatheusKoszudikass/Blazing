
namespace BlazingPizzaria.Models.DTOs
{
    public class RevisaoDto
    {
        public Guid Id { get; set; }
        public string? Usuario { get; set; }
        public string? Comentario { get; set; }
        public DateTime Data { get; set; }
    }
}
