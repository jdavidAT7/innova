using System;

namespace back_end.DTOs
{
    public class ProveedorContratoDTO
    {
        public int Id { get; set; }
        public string CodigoContrato { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string TipoContrato { get; set; }
        public string Documento { get; set; }
    }
}
