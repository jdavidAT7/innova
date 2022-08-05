namespace back_end.DTOs
{
    public class ClientesFiltrarDTO
    {
        public int Pagina { get; set; }
        public int RecordsPorPagina { get; set; }
        public PaginacionDTO PaginacionDTO
        {
            get { return new PaginacionDTO() { Pagina = Pagina, RecordsPorPagina = RecordsPorPagina }; }
        }
        public string NombreComercial { get; set; }
        public int AdendumId { get; set; }
        public bool Estado { get; set; }
        public bool SiguientesRegistros { get; set; }
    }
}
