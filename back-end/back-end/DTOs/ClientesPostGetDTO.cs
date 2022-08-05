using System.Collections.Generic;

namespace back_end.DTOs
{
    public class ClientesPostGetDTO
    {
        public List<ClienteAdendumDTO> Adendums { get; set; }
        public List<ServicioDTO> Servicios { get; set; }
    }
}
