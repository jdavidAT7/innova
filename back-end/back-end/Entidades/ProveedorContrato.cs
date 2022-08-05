using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class ProveedorContrato
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]
        public string CodigoContrato { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string TipoContrato { get; set; }
        public string Documento { get; set; }

    }
}
