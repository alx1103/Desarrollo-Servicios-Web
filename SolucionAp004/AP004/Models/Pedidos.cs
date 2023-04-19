namespace AP004.Models
{
    public class Pedidos
    {
        public int idPedido { get; set; }
        public DateTime fechaPedido { get; set; } = DateTime.Now;
        public string? nomCliente { get; set; }
        public string? cuidadDestinatario { get; set; }
        public string? paisDestinatario { get; set; }
    }
}
