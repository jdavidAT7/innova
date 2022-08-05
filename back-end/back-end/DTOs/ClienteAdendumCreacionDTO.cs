using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs
{
    public class ClienteAdendumCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string CodigoAdendum { get; set; }
        public DateTime FechaAdendum { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Observacion { get; set; }
        public IFormFile Documento { get; set; }
    }
}
