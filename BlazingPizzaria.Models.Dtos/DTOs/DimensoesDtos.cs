using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizzaria.Models.DTOs
{
    public class DimensoesDtos
    {
        public int Id { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Profundidade { get; set; }
        public string Unidade { get; set; } = "cm";
    }
}
