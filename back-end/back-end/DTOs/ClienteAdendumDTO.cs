using System.ComponentModel.DataAnnotations;
using System;

namespace back_end.DTOs
{
    public class ClienteAdendumDTO
    {
        public int Id { get; set; }
        public string CodigoAdendum { get; set; }
        public DateTime FechaAdendum { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Observacion { get; set; }
        public string Documento { get; set; }
    }
}
