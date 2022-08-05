using System.Collections.Generic;

namespace back_end.DTOs
{
    public class ClientesPutGetDTO
    {
        public ClienteDTO Cliente { get; set; }
        public List<ClienteAdendumDTO> AdendumsSeleccionados { get; set; }
        public List<ClienteAdendumDTO> AdendumsNoSeleccionados { get; set; }
        public List<ServicioDTO> ServiciosSeleccionados { get; set; }
        public List<ServicioDTO> ServiciosNoSeleccionados { get; set; }
        public List<CContratoDTO> Contratos { get; set; }

    }
}
