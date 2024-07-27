using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizzaria.Models.DTOs
{
    public class DisponibilidadeDto
    {
        [Key]
        public Guid Id { get; set; }
        public bool EstaDisponivel { get; set; }
        public DateTime DataEstimadaDeEntrega { get; set; }
    }
}
