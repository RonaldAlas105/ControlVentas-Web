namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Proveedor en la base de datos SQL
     /// </summary>
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public byte Estado { get; set; }

    }
}
