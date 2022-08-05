using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace back_end.Entidades
{
    public class ProveedorAdendum
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string CodigoAdendum { get; set; }
        public DateTime FechaAdendum { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Observacion { get; set; }
        public string Documento { get; set; }

    }
}
