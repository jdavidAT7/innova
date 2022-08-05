using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class Servicio
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string CodigoServicio { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }

        public List<ClientesServicios> ClientesServicios { get; set; }
    }
}
