using System;

namespace back_end.DTOs
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public string CodigoServicio { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
