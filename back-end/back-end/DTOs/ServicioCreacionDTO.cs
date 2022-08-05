using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

namespace back_end.DTOs
{
    public class ServicioCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string CodigoServicio { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        
    }
}
