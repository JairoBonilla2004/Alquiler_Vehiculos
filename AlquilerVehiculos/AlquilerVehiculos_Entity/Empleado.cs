namespace AlquilerVehiculos_Entity
{
    public class Empleado
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Email {  get; set; }
        public string Clave { get; set; }
        public bool Restablecer_clave { get; set; }
        public bool Confirmado { get; set; }
        public string Token {  get; set; }
    }
}
