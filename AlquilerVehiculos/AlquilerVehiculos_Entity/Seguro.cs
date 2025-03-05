namespace AlquilerVehiculos_Entity
{
    public class Seguro
    {
        public int Id { get; set; }
        public int ReservaId { get; set; } 
        public string TipoSeguro { get; set; } 
        public decimal Costo { get; set; }

    
        public string ClienteNombre { get; set; } 
        public string VehiculoNombre { get; set; } 
    }
}
