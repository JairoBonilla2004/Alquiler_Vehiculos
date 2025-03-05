namespace AlquilerVehiculos_Entity
{
    public class Pago
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } 
        public DateTime FechaPago { get; set; }
    }
}
