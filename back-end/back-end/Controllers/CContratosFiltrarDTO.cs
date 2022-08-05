using back_end.DTOs;

namespace back_end.Controllers
{
    public class CContratosFiltrarDTO
    {
        public int Pagina { get; set; }
        public int RecordsPorPagina { get; set; }
        public PaginacionDTO PaginacionDTO
        {
            get { return new PaginacionDTO() { Pagina = Pagina, RecordsPorPagina = RecordsPorPagina }; }
        }
        public string CodigoContrato { get; set; }
        public int AdendumId { get; set; }
        public bool Estado { get; set; }
        public bool SiguientesRegistros { get; set; }
    }
}
