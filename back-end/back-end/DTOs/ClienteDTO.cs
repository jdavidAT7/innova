using System.Collections.Generic;

namespace back_end.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        //public bool Estado { get; set; }  //nuevo
        public string ContactoCliente { get; set; }
        public string Telefono { get; set; }
        public string Nit { get; set; }
        public string Email { get; set; }

        public List<ClienteAdendumDTO> Adendums { get; set; }
        public List<CContratoDTO> Contratos { get; set; }
        public List<ServicioDTO> Servicios { get; set; }
    }
}
