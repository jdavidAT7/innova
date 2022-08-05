using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class CContratos
    {
        public int ClienteId { get; set; }
        public int ContratoId { get; set; }
        public Cliente Cliente { get; set; }
        public ClienteContrato Contrato { get; set; }

        [StringLength(maximumLength: 100)]
        public string Personaje { get; set; }
        public int Orden { get; set; }
    }
}
