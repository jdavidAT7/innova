namespace back_end.Entidades
{
    public class ClientesAdendums
    {
        public int ClienteId { get; set; }
        public int AdendumId { get; set; }
        public Cliente Cliente { get; set; }
        public ClienteAdendum Adendum { get; set; }
    }
}
