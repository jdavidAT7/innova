using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using back_end.Utilidades;
using System.Collections.Generic;
namespace back_end.DTOs
{
    public class ProveedorContratoCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string CodigoContrato { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string TipoContrato { get; set; }
        public IFormFile Documento { get; set; }
    }
}
