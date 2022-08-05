namespace back_end.Entidades
{
    public class ClientesServicios
    {
        public int ClienteId { get; set; }
        public int ServicioId { get; set; }
        public Cliente Cliente { get; set; }
        public Servicio Servicio { get; set; }
    }
}
